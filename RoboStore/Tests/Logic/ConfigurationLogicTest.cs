using System.Linq;
using System.Threading.Tasks;
using Domain.Configurations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Logic
{
  /// <summary>
  // Проверяем получение и передачу категорий
  /// </summary>
  [TestClass]
  public class ConfigurationLogicTest: LogicTest
  {
    
    private readonly ConfigurationManager _manager;

    public ConfigurationLogicTest()
    {
      _manager = new ConfigurationManager(data);
    }

    [TestMethod]
    public void GetConfigurationsByRobotIdTest()
    {
      var result = _manager.GetConfigurationsByRobotId(data.Robots.First().RobotID);
      Assert.AreEqual(1, result.Count());
      Assert.AreEqual(data.Configurations.First().Port, result.First().Port);
    }

    [TestMethod]
    public async Task CreateConfigurationTest()
    {
      var config = data.Configurations.First();
      await _manager.CreateConfiguration(new ConfigurationImport {Port = config.Port, RobotID = config.RobotID});
      Assert.AreEqual(2, data.Configurations.Count());
      Assert.AreEqual(config.Port, data.Configurations.Last().Port);
      Assert.AreEqual(config.RobotID, data.Configurations.Last().RobotID);
    }
  }
}
