using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Models.Managers;
using Tests.Services;

namespace Tests.Logic
{
  [TestClass]
  public class ProgramLogicTest : LogicTest
  {
    private readonly ProgramManager _manager;

    public ProgramLogicTest()
    {
      _manager = new ProgramManager(data, new FakeRobotConnector());
    }

    [TestMethod]
    public void GetProgram()
    {
      var prg = data.Programs.First();
      var result = _manager.GetProgramById(prg.ProgramID);
      Assert.AreNotEqual(null, result);
      Assert.AreEqual(result.Code, prg.Code);
      Assert.AreEqual(result.Name, prg.Name);
      Assert.AreEqual(result.ActualVersion, prg.ActualVersion);
    }

    [TestMethod]
    public void GetProgramsIds()
    {
      var robot = data.Robots.First();
      var result = _manager.GetProgramsIds(robot.RobotID);
      Assert.AreEqual(1, result.Count());
      var robotProgram = data.ProgramRobots.First(x => x.ProgramID == result.First().Id);
      Assert.AreNotEqual(robotProgram, null);
      Assert.AreEqual(robot.RobotID, robotProgram.RobotID);
    }
  }
}
