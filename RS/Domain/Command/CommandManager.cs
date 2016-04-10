using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Data;
using Domain.Entities;

namespace Domain.Command
{
  public class CommandManager
  {
    private IData data;

    public CommandManager(IData d)
    {
      data = d;
    }

    public void AskRobotAboutProgram(Robot robot, Program program, RobotCommandTypes commandType)
      => data.RobotCommands.Add(new RobotCommand
      {
        Argument = program.ProgramID,
        Robot = robot,
        Type = (int) commandType
      });

    public IEnumerable<RobotCommand> GetRobotCommandsByRobotId(int robotId)
      => data.RobotCommands.Data.Where(x => x.RobotID == robotId && x.Received == false);

    public void SetCommandGot(int commandId)
    {
      var command = data.RobotCommands.Data.First(x => x.RobotCommandID == commandId);
      command.Received = true;
      data.RobotCommands.Update(command);
    }

    public void RemoveExecutedProgram(int commandId)
    {
      var command = data.RobotCommands.Data.First(x => x.RobotCommandID == commandId);
      data.RobotCommands.Remove(command);
    }

  }
}
