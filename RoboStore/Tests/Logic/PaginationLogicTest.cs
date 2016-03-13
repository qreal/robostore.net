using System.Linq;
using Domain;
using Domain.Pagination;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Logic
{
  [TestClass]
  public class PaginationLogicTest : LogicTest
  {
    [TestMethod]
    public void FormListLogicTest()
    {
      var _manager = new PaginationManager(data);
      var result = _manager.FormProgramPage(pageSize: 2, page: 1);
      var programs = data.Programs.OrderBy(x => x.Name).Take(2);
      Assert.AreEqual(result.Count(), 2);
      Assert.AreEqual(result.First().Name, programs.First().Name);
      Assert.AreEqual(result.Last().Name, programs.Last().Name);

      var result2 = _manager.FormRobotPage(pageSize: 2, page: 1);
      var robots = data.Robots.OrderBy(x => x.RobotID).Take(1);
      Assert.AreEqual(result2.Count(), 1);
      Assert.AreEqual(result2.First().RobotID, robots.First().RobotID);
    }
  }
}
