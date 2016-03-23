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

    public async Task AskRobotAboutProgram(Robot robot, Program program, RobotCommandTypes commandType)
      => await data.AddAsync(new RobotCommand
      {
        Argument = program.ProgramID,
        Robot = robot,
        Type = (int) commandType
      });
  }
}
