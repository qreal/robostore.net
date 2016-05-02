using System.Linq;
using Domain.Entities;
using Domain.Managers.RobotCommand;
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
    public void AskRobotInstallProgramLogicTest()
    {
      var robot = data.Robots.Data.First();
      var program = data.Programs.Data.First();

      CheckOneTypeCommand(robot, program, RobotCommandTypes.Install);
      CheckOneTypeCommand(robot, program, RobotCommandTypes.Remove);
      CheckOneTypeCommand(robot, program, RobotCommandTypes.Update);
    }

    [TestMethod]
    public void GetRobotCommandsByRobotIdLogicTest()
    {
      // case 1 : program is NOT currenty reveived by robot
      var robot = data.Robots.Data.First();
      var command = data.RobotCommands.Data.First();
      var result = _manager.GetNotReceivedCommandsByRobotId(robot.RobotID);
      Assert.AreSame(command, result.First());
      Assert.AreEqual(1, result.Count());

      // case 2 : program is currenty reveived by robot
      command.Received = true;
      result = _manager.GetNotReceivedCommandsByRobotId(robot.RobotID);
      Assert.AreEqual(0, result.Count());
    }

    private void CheckOneTypeCommand(Robot robot, Program program,  RobotCommandTypes type)
    {
      var amount = data.RobotCommands.Data.Count();
      _manager.CreateRobotCommand(robot, program, type);
      var command = data.RobotCommands.Data.Last();
      Assert.AreEqual(amount + 1, data.RobotCommands.Data.Count());
      Assert.AreEqual(command.Argument, program.ProgramID);
      Assert.AreSame(command.Robot, robot);
      Assert.AreEqual(command.Type, (int) type);
    }

    [TestMethod]
    public void SetCommandGotLogicTest()
    {
      var command = data.RobotCommands.Data.First();
      _manager.SetCommandReceived(command.RobotCommandID);
      Assert.AreEqual(true, command.Received);
    }

    [TestMethod]
    public void RemoveExecutedProgramAsyncLogicTest()
    {
      var amount = data.RobotCommands.Data.Count();
      var command = data.RobotCommands.Data.First();
      var id = command.RobotCommandID;
      _manager.RemoveExecutedProgram(command.RobotCommandID);
      Assert.AreEqual(amount - 1, data.RobotCommands.Data.Count());
      Assert.AreEqual(null, data.RobotCommands.Data.FirstOrDefault(x => x.RobotCommandID == id));
    }
  }
}
