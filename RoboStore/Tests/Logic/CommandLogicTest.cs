using System.Linq;
using System.Threading.Tasks;
using Domain.Command;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Logic
{
  [TestClass]
  public class CommandLogicTest : LogicTest
  {
    private CommandManager _manager;

    public CommandLogicTest()
    {
      _manager = new CommandManager(data);
    }
    [TestMethod]
    public async Task AskRobotInstallProgramTest()
    {
      var robot = data.Robots.Data.First();
      var program = data.Programs.Data.First();

      await CheckOneTypeCommand(robot, program, RobotCommandTypes.Install);
      await CheckOneTypeCommand(robot, program, RobotCommandTypes.Remove);
      await CheckOneTypeCommand(robot, program, RobotCommandTypes.Update);
    }

    [TestMethod]
    public void GetRobotCommandsByRobotIdTest()
    {
      var robot = data.Robots.Data.First();
      var command = data.RobotCommands.Data.First();
      var result = _manager.GetRobotCommandsByRobotId(robot.RobotID);
      Assert.AreSame(command, result.First());
      Assert.AreEqual(1, result.Count());
    }

    private async Task CheckOneTypeCommand(Robot robot, Program program,  RobotCommandTypes type)
    {
      var amount = data.RobotCommands.Data.Count();
      await _manager.AskRobotAboutProgramAsync(robot, program, type);
      var command = data.RobotCommands.Data.Last();
      Assert.AreEqual(amount + 1, data.RobotCommands.Data.Count());
      Assert.AreEqual(command.Argument, program.ProgramID);
      Assert.AreSame(command.Robot, robot);
      Assert.AreEqual(command.Type, (int) type);
    }
  }
}
