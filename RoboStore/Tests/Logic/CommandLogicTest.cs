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
      var robot = data.Robots.First();
      var program = data.Programs.First();

      await CheckOneTypeCommand(robot, program, RobotCommandTypes.Install);
      await CheckOneTypeCommand(robot, program, RobotCommandTypes.Remove);
      await CheckOneTypeCommand(robot, program, RobotCommandTypes.Update);
    }

    private async Task CheckOneTypeCommand(Robot robot, Program program,  RobotCommandTypes type)
    {
      var amount = data.RobotCommands.Count();
      await _manager.AskRobotAboutProgram(robot, program, type);
      var command = data.RobotCommands.Last();
      Assert.AreEqual(amount + 1, data.RobotCommands.Count());
      Assert.AreEqual(command.Argument, program.ProgramID);
      Assert.AreSame(command.Robot, robot);
      Assert.AreEqual(command.Type, (int) type);
    }
  }
}
