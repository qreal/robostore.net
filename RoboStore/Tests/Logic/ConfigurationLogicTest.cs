using System.Linq;
using System.Threading.Tasks;
using Domain.Managers.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Logic
{
  /// <summary>
  // Проверяем получение и передачу категорий
  /// </summary>
  [TestClass]
  public class ConfigurationLogicTest: LogicTest
  {
    
    private readonly ConfigurationManager _manager;

    public ConfigurationLogicTest()
    {
      _manager = new ConfigurationManager(data);
    }

    [TestMethod]
    public void GetConfigurationsByRobotIdLogicTest()
    {
      var result = _manager.GetConfigurationsByRobotId(data.Robots.Data.First().RobotID);
      Assert.AreEqual(1, result.Count());
      Assert.AreEqual(data.Configurations.Data.First().Port, result.First().Port);
    }

    [TestMethod]
    public void CreateConfigurationLogicTest()
    {
      var config = data.Configurations.Data.First();
      _manager.CreateConfiguration(new ConfigurationImport {Port = config.Port, RobotID = config.RobotID});
      Assert.AreEqual(2, data.Configurations.Data.Count());
      Assert.AreEqual(config.Port, data.Configurations.Data.Last().Port);
      Assert.AreEqual(config.RobotID, data.Configurations.Data.Last().RobotID);
    }
  }
}
