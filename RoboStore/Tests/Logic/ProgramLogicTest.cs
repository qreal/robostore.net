using System.Linq;
using System.Threading.Tasks;
using Domain.Programs;
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
    public void GetProgramByIdTest()
    {
      var prg = data.Programs.First();
      var result = _manager.GetProgramById(prg.ProgramID);
      Assert.AreNotEqual(null, result);
      Assert.AreEqual(result.Code, prg.Code);
      Assert.AreEqual(result.Name, prg.Name);
      Assert.AreEqual(result.ActualVersion, prg.ActualVersion);
    }

    [TestMethod]
    public void GetProgramsIdsByRobotIdTest()
    {
      var robot = data.Robots.First();
      var result = _manager.GetProgramsIdsByRobotId(robot.RobotID);
      Assert.AreEqual(1, result.Count());
      var robotProgram = data.ProgramRobots.First(x => x.RobotID == robot.RobotID);
      Assert.AreEqual(result.First().ProgramID, robotProgram.ProgramID);
    }

    [TestMethod]
    public async Task AddProgramToRobotTest()
    {
      var robot = data.Robots.First();
      var program = data.Programs.First();
      int before = data.ProgramRobots.Count();
      await _manager.AddProgramToRobot(program.ProgramID, robot.RobotID);
      Assert.AreEqual(before + 1, data.ProgramRobots.Count());
      var programRobot = data.ProgramRobots.Last();
      Assert.AreEqual(programRobot.RobotID, robot.RobotID);
      Assert.AreEqual(programRobot.Program, program);
    }
  }
}
