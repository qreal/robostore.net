using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Domain.Command;
using Domain.Data;
using Domain.Programs;
using Domain.Robots;
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
    public async Task<RobotRegistrationOutput> RegisterRobot()
    {
      var robot = await robotManager.CreateRobot();
      return new RobotRegistrationOutput
      {
        RobotId = robot.RobotID,
        ActivateCode = robot.ActivationCode
      };
    }

    [Route("api/GetCommands")]
    [HttpGet]
    public IEnumerable<GetCommandsOutput> GetCommands(int robotId)
      => commandManager.GetRobotCommandsByRobotId(robotId).Select(x => new GetCommandsOutput
      {
        Argument = x.Argument,
        RobotCommandID = x.RobotCommandID,
        Type = x.Type
      });

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
      
    
  }
}
