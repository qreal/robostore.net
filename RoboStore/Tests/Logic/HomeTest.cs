using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Models.Entities;
using Store.Models.Managers;
using Store.ViewModels.Home;

namespace Tests.Logic
{
  [TestClass]
  public class HomeTest : LogicTest
  {
    private ControllerManager _manager;

    public HomeTest()
    {
      _manager = new ControllerManager(data, robotConnector);
    }

    [TestMethod]
    public void FormProgramListLogicTest()
    {
      ContentListViewModel<Program> result = _manager.FormProgramList ( pageSize: 2, page: 1);
      var programs = data.Programs.OrderBy(x => x.Name).Take(2);
      Assert.AreEqual(result.Content.Count(),2);
      Assert.AreEqual(result.Content.First().Name, programs.First().Name);
      Assert.AreEqual(result.Content.Last().Name, programs.Last().Name);
      Assert.AreEqual(result.PagingInfo.CurrentPage, 1);
      Assert.AreEqual(result.PagingInfo.ItemsPerPage, 2);
      Assert.AreEqual(result.PagingInfo.TotalItems, 5);
      Assert.AreEqual(result.PagingInfo.TotalPages, 3);
    }

    [TestMethod]
    public async Task AddProgramToRobotLogicTest()
    {
      int amountRobotProgramBefore = data.ProgramRobots.Count();
      var robot = data.Robots.First();
      var program = data.Programs.First();
      await _manager.AddProgramToRobot(program.ProgramID, robot.RobotID);
      Assert.AreEqual(amountRobotProgramBefore + 1, data.ProgramRobots.Count());
      var robotProgram = data.ProgramRobots.Last();
      Assert.AreEqual(robotProgram.RobotID, robot.RobotID);
      Assert.AreEqual(robotProgram.Program, program);
      Assert.AreEqual(robotProgram.CurrentVersion, program.ActualVersion);
      Assert.AreEqual(1, robotConnector.couterConnections);
    }
  }
}
