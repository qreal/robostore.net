using System.Threading.Tasks;
using System.Web.Http;
using Domain.Data;
using Domain.Robots;
using Store.Models.API;

namespace Store.Controllers
{
  public class APIController : ApiController
  {
    private RobotManager robotManager;

    public APIController(IData data)
    {
      robotManager = new RobotManager(data);
    }

    [Route("api/robotRegistration")]
    [HttpGet]
    public async Task<RobotRegistrationOutput> RegistraterRobot()
    {
      var robot = await robotManager.CreateRobot();
      return new RobotRegistrationOutput
      {
        RobotId = robot.RobotID,
        ActivateCode = robot.ActivationCode
      };
    }
  }
}
