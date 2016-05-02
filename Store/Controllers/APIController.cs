using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Domain.Data;
using Domain.Managers;
using Domain.Managers.RobotCommand;
using Store.Models.API;
using Store.Models.Command;
using Store.Models.Program;

namespace Store.Controllers
{
  public class APIController : ApiController
  {
    private RobotManager robotManager;
    private CommandManager commandManager;
    private ProgramManager programManager;

    public APIController(IData data)
    {
      robotManager = new RobotManager(data);
      commandManager = new CommandManager(data);
      programManager = new ProgramManager(data);
    }

    [Route("api/robotRegistration")]
    [HttpGet]
    public RobotRegistrationOutput RegisterRobot()
    {
      var robot = robotManager.CreateRobot();
      return new RobotRegistrationOutput
      {
        RobotId = robot.RobotID,
        ActivateCode = robot.ActivationCode
      };
    }

    [Route("api/GetCommands")]
    [HttpGet]
    public IEnumerable<GetCommandsOutput> GetCommands(int robotId)
    {
      return commandManager.GetNotReceivedCommandsByRobotId(robotId).Select(x => new GetCommandsOutput
      {
        Argument = x.Argument,
        RobotCommandID = x.RobotCommandID,
        Type = x.Type
      });
    }


    [Route("api/GetProgram")]
    [HttpGet]
    public ProgramExport GetProgram(int programId)
    {
      var program = programManager.GetProgramById(programId);
      return new ProgramExport
      {
        Code = program.Code,
        Name = program.Name,
        ProgramId = programId
      };
    }

    [Route("api/ReportCommandGot")]
    [HttpPost]
    public void ReportCommandGot(ReportCommandGotInput model)
      => commandManager.SetCommandReceived(model.CommandId);

    [Route("api/ReportCommandExecuted")]
    [HttpPost]
    public void ReportCommandExecuted(ReportCommandExecutedInput model)
      => commandManager.RemoveExecutedProgram(model.CommandId);

  }
}
