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

    public async Task AskRobotInstallProgramAsync(Robot robot, Program program)
      => await data.AddAsync(new RobotCommand
      {
        Argument = program.ProgramID,
        Robot = robot,
        Type = (int) RobotCommandTypes.Install
      });
  }
}
