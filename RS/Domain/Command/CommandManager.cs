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

    public void AskRobotInstallProgram(Robot robot, Program program)
    {
      
    }
  }
}
