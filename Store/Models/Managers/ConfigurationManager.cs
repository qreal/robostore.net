using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Models.Data;
using Store.Models.Entities;
using Store.ViewModels.Configuration;

namespace Store.Models.Managers
{
  public class ConfigurationManager
  {
    private IData data;

    public ConfigurationManager(IData d)
    {
      data = d;
    }

    public IEnumerable<ConfigurationExport> GetConfigurations(int robotId) 
      => data.Configurations.Where(x => x.RobotID == robotId).
      Select(x => new ConfigurationExport { Port = x.Port });

    public async Task CreateConfiguration(ConfigurationImport configuration) =>
      await data.AddAsync(new Configuration
      {
        Port = configuration.Port,
        RobotID = configuration.RobotID
      });

  }
}
