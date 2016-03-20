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
      var id = data.Programs.First().ProgramID;
      Assert.AreSame(_manager.GetProgramById(id), data.Programs.First());
    }

    [TestMethod]
    public async Task CreateProgramRobotTest()
    {
      var amount = data.ProgramRobots.Count();
      var program = data.Programs.First();
      var robot = data.Robots.First();
      await _manager.CreateProgramRobot(robot, program);
      var result = data.ProgramRobots.Last();
      Assert.AreEqual(amount + 1, data.ProgramRobots.Count());
      Assert.AreSame(program, result.Program);
      Assert.AreSame(robot, result.Robot);
      Assert.AreEqual(result.CurrentVersion, program.ActualVersion);
    }
  }
}
