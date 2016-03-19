using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Store.Models.API;
using Tests.DB;

namespace Tests.API
{
  [TestClass]
  public class APIControllerTest : APITest
  {
    [TestMethod]
    public async Task RegistraterRobotTest()
    {
      Robot last = null;
      var amount = context.Robots.Count();
      try
      {
        // регистрация нового робота
        using (var wb = new WebClient())
        {
          wb.Encoding = Encoding.UTF8;
          var json = wb.DownloadString(serverUrl + "/api/robotRegistration");
          var resp = JsonConvert.DeserializeObject<RobotRegistrationOutput>(json);
          Assert.AreEqual(amount + 1, context.Robots.Count());
          last = context.Robots.ToList().Last();
          Assert.AreEqual(resp.ActivateCode, last.ActivationCode);
          Assert.AreEqual(resp.RobotId, last.RobotID);
        }
      }
      finally
      {
        context.Robots.Remove(last);
        await context.SaveChangesAsync();
        Assert.AreEqual(amount, context.Robots.Count());
      }
      

    }
  }
}
