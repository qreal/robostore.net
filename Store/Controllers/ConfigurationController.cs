using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Store.Models.Data;
using Store.Models.Managers;
using Store.ViewModels.Configuration;

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
    public IEnumerable<ConfigurationExport> GetConfigurations(int robotId) => _manager.GetConfigurations(robotId);

    [Route("api/configuration/post")]
    [HttpPost]
    public async Task CreateConfiguration(ConfigurationImport conf) => await _manager.CreateConfiguration(conf);
  }
}
