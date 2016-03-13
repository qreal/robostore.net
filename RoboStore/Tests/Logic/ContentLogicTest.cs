using System.Linq;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Services;

namespace Tests.Logic
{
  [TestClass]
  public class ContentLogicTest: LogicTest
  {
    [TestMethod]
    public void GetAmountTest()
    {
      var manager = new ContentManager(data);
      Assert.AreEqual(5, manager.AmountPrograms);
      Assert.AreEqual(1, manager.AmoutRobots);
    }

    [TestMethod]
    public void GetImageByIdTest()
    {
      var manager = new ContentManager(data);
      var bytes = manager.GetImageById(1).ImageData;
      var eBytes = MoqDataGenerator.GetSomeBytes().ToArray();
      var i = 0;
      foreach (var b in bytes)
      {
        Assert.AreEqual(b, eBytes[i++]);
      }
    }
  }
}
