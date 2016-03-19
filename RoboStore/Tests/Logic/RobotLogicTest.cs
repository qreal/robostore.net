using System.Linq;
using System.Threading.Tasks;
using Domain.Robots;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Logic
{
  [TestClass]
  public class RobotLogicTest : LogicTest
  {
    private readonly RobotManager _manager;

    public RobotLogicTest()
    {
      _manager = new RobotManager(data);
    }

    [TestMethod]
    public async Task CreateRobotTest()
    {
      var amount = data.Robots.Count();
      var result = await _manager.CreateRobot();
      Assert.AreEqual(amount + 1, data.Robots.Count());
      Assert.AreEqual(result.RobotID, 0);
      Assert.AreEqual(true, result.ActivationCode >= RobotManager.ActivationCodeMin
        && result.ActivationCode <= RobotManager.ActivationCodeMax);
    }
  }
}
