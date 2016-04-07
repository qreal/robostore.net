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

    public async Task AskRobotAboutProgramAsync(Robot robot, Program program, RobotCommandTypes commandType)
      => await data.RobotCommands.AddAsync(new RobotCommand
      {
        Argument = program.ProgramID,
        Robot = robot,
        Type = (int) commandType
      });

    public IEnumerable<RobotCommand> GetRobotCommandsByRobotId(int robotId)
      => data.RobotCommands.Data.Where(x => x.RobotID == robotId);

    public async Task SetCommandGotAsync(int commandId)
    {
      var command = data.RobotCommands.Data.First(x => x.RobotCommandID == commandId);
      command.Received = true;
      await data.RobotCommands.UpdateAsync(command);
    }

    public async Task RemoveExecutedProgramAsync(int commandId)
      => await data.RobotCommands.RemoveAsync(data.RobotCommands.Data.First(x => x.RobotCommandID == commandId));

  }
}
