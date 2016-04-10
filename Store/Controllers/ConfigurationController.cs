using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Domain.Configurations;
using Domain.Data;
using Store.Models.Configuration;

namespace Store.Controllers
{
  public class ConfigurationController : ApiController
  {
    private readonly ConfigurationManager _manager;

    public ConfigurationController(IData data)
    {
      _manager = new ConfigurationManager(data);
    }

    [Route("api/configuration/get")]
    [HttpGet]
    public IEnumerable<ConfigurationExport> GetConfigurations(int robotId) => 
      _manager.GetConfigurationsByRobotId(robotId).
      Select(x => new ConfigurationExport { Port = x.Port });

    [Route("api/configuration/post")]
    [HttpPost]
    public void CreateConfiguration(ConfigurationImport conf) => _manager.CreateConfiguration(conf);
  }
}
