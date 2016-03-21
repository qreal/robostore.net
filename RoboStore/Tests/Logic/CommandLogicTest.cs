using System.Linq;
using System.Threading.Tasks;
using Domain.Command;
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
      var amount = data.RobotCommands.Count();
      await _manager.AskRobotInstallProgramAsync(robot, program);
      var command = data.RobotCommands.Last();
      Assert.AreEqual(amount + 1, data.RobotCommands.Count());
      Assert.AreEqual(command.Argument, program.ProgramID);
      Assert.AreSame(command.Robot, robot);
      Assert.AreEqual(command.Type, (int) RobotCommandTypes.Install);

    }
  }
}
