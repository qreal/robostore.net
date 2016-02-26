using System;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Store.ViewModels.Program;
using Tests.DB;
using Tests.Services;

namespace Tests.API
{
  [TestClass]
  public class ProgramAPITest : APITest
  {
    [TestMethod]
    public void GetProgram()
    {
      var image = new Image
      {
        ImageData = MoqDataGenerator.GetSomeBytes(),
        ImageMimeType = MoqDataGenerator.GetRandomString(6)
      };
      context.Images.Add(image);
      context.SaveChanges();

      // создаем новую прогу в бд
      var program = new Program
      {
        ActualVersion = MoqDataGenerator.GetRandomNumber(1, 11),
        Code = MoqDataGenerator.GetRandomString(100),
        Name = MoqDataGenerator.GetRandomString(10),
        Image = image
      };
      context.Programs.Add(program);
      context.SaveChanges();

      try
      {
        // делаем запрос на получение программы
        using (var wb = new WebClient())
        {
          wb.Encoding = Encoding.UTF8;
          var json = wb.DownloadString(serverUrl + "/program/getProgramById?id=" + program.ProgramID);
          ProgramExport resp = JsonConvert.DeserializeObject<ProgramExport>(json);
          Assert.AreEqual(program.ActualVersion, resp.ActualVersion);
          Assert.AreEqual(program.Code, resp.Code);
          Assert.AreEqual(program.Name, resp.Name);
        }
      }
      finally
      {
        // удаляем новую прогу из БД (каскадно)
        context.Images.Remove(image);
        context.SaveChanges();
      }  
    }
  }
}
