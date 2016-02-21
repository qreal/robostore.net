using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Store.ViewModels.Configuration;
using Tests.DB;
using Tests.Services;

namespace Tests.API
{
  [TestClass]
  public class ConfigurationAPITest
  {
    /*
    Тестируем 2 функции контроллера - получить конфигурации и добавить новую
    */

    private RobotsDBEntities context = new RobotsDBEntities();
    private Robot robot;
    private List<Configuration> configurations = new List<Configuration>();
    private string serverUrl = "http://robstore.azurewebsites.net/api";



    [TestMethod]
    public void GetConfigurationsAPITest()
    {
      // создали тестовые данные
      GenerateTestData();

      // делаем запрос на получение Конфиг для нового Робота
      using (var wb = new WebClient())
      {
        wb.Encoding = Encoding.UTF8;
        var json = wb.DownloadString(serverUrl + "/configuration/get?RobotId=" + robot.RobotID);
        ConfigurationExport[] resp = JsonConvert.DeserializeObject<ConfigurationExport[]>(json);
        Assert.AreEqual(2, resp.Length);
        Assert.AreEqual(resp[0].Port, configurations[0].Port);
        Assert.AreEqual(resp[1].Port, configurations[1].Port);
      }

      // удалили тестовые данные
      RemoveTestData();
    }

    [TestMethod]
    public void CreateConfigurationAPITest()
    {
      // запомнили кол-во и создали тестового робота
      int amountBefore = context.Configurations.Count();
      int port = MoqDataGenerator.GetRandomNumber(1, 111);
      var robot = new Robot();
      context.Robots.Add(robot);
      context.SaveChanges();

      using (var wb = new WebClient())
      {
        var data = new NameValueCollection();
        data["Port"] = port.ToString();
        data["RobotID"] = robot.RobotID.ToString();
        wb.UploadValues(serverUrl + "/configuration/post", "POST", data);
      }

      Assert.AreEqual(amountBefore + 1, context.Configurations.Count());
      Assert.AreEqual(port, context.Configurations.ToList().Last().Port);

      context.Robots.Remove(robot);
      context.SaveChanges();
    }

    private void GenerateTestData()
    {
      robot = new Robot();
      context.Robots.Add(robot);
      context.SaveChanges();

      configurations.Add(new Configuration
      {
        Port = MoqDataGenerator.GetRandomNumber(1, 1111),
        Robot = robot
      });
      configurations.Add(new Configuration
      {
        Port = MoqDataGenerator.GetRandomNumber(1, 1111),
        Robot = robot
      });
      context.Configurations.AddRange(configurations);
      context.SaveChanges();
    }

    private void RemoveTestData()
    {
      context.Robots.Remove(robot);
      context.SaveChanges();
    }
  }
}
