using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Data;

namespace Domain.Configurations
{
  public class ConfigurationManager
  {
    private IData data;

    public ConfigurationManager(IData d)
    {
      data = d;
    }

    public IEnumerable<Entities.Configuration> GetConfigurationsByRobotId(int robotId)
      => data.Configurations.Where(x => x.RobotID == robotId);

    public async Task CreateConfiguration(ConfigurationImport configuration) =>
      await data.AddAsync(new Entities.Configuration
      {
        Port = configuration.Port,
        RobotID = configuration.RobotID
      });

  }
}
