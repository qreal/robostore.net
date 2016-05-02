using System.Linq;
using Domain.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Logic
{
  [TestClass]
  public class ProgramLogicTest : LogicTest
  {
    private readonly ProgramManager _manager;

    public ProgramLogicTest()
    {
      _manager = new ProgramManager(data);
    }

    [TestMethod]
    public void GetProgramByIdLogicTest()
    {
      var id = data.Programs.Data.First().ProgramID;
      Assert.AreSame(_manager.GetProgramById(id), data.Programs.Data.First());
    }

    [TestMethod]
    public void CreateProgramRobotLogicTest()
    {
      var amount = data.ProgramRobots.Data.Count();
      var program = data.Programs.Data.First();
      var robot = data.Robots.Data.First();
      _manager.CreateProgramRobot(robot, program);
      var result = data.ProgramRobots.Data.Last();
      Assert.AreEqual(amount + 1, data.ProgramRobots.Data.Count());
      Assert.AreSame(program, result.Program);
      Assert.AreSame(robot, result.Robot);
      Assert.AreEqual(result.CurrentVersion, program.ActualVersion);
    }

    [TestMethod]
    public void GetRobotProgramsByRobotIdLogicTest()
    {
      var robot = data.Robots.Data.First();
      var result = _manager.GetRobotProgramsByRobotId(robot.RobotID);
      Assert.AreEqual(1, result.Count());
      Assert.AreSame(result.First(), data.ProgramRobots.Data.First());
    }

    [TestMethod]
    public void UpdateProgramRobotLogicTest()
    {
      var programRobot = data.ProgramRobots.Data.First();
      var program = data.Programs.Data.First();
      _manager.UpdateVersionProgramRobot(programRobot.ProgramRobotID);
      Assert.AreEqual(program.ActualVersion, programRobot.CurrentVersion);
    }

    [TestMethod]
    public void RemoveProgramRobotLogicTest()
    {
      var programRobot = data.ProgramRobots.Data.First();
      var amount = data.ProgramRobots.Data.Count();
      _manager.RemoveProgramRobot(programRobot.ProgramRobotID);
      Assert.AreEqual(amount - 1, data.ProgramRobots.Data.Count());
    }

    [TestMethod]
    public void GetProgramByProgramRobotIdLogicTest()
    {
      var programRobot = data.ProgramRobots.Data.First();
      var program = data.Programs.Data.First();
      var result = _manager.GetProgramByProgramRobotId(programRobot.ProgramRobotID);
      Assert.AreSame(program, result);
    }
  }
}
