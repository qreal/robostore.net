using System.Linq;
using Domain;
using Domain.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Logic
{
  [TestClass]
  public class ContentLogicTest: LogicTest
  {
    private ContentManager _manager;

    public ContentLogicTest()
    {
      _manager = new ContentManager(data);
    }
    [TestMethod]
    public void GetAmountLogicTest()
    {
      Assert.AreEqual(5, _manager.AmountPrograms);
      Assert.AreEqual(1, _manager.AmoutRobots);
    }

    [TestMethod]
    public void GetImageByIdLogicTest()
    {
      var image = data.Images.Data.First();
      Assert.AreSame(image, _manager.GetImageById(image.ImageID));
    }
  }
}
