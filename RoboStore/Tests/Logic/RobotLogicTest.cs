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
      Assert.AreEqual(result.RobotID + result.ActivationCode, 0);
    }

    [TestMethod]
    public void GetRobotByActivationCodeTest()
    {
      var code = data.Robots.First().ActivationCode;
      Assert.AreSame(data.Robots.First(), _manager.GetRobotByActivationCode(code));
    }

    [TestMethod]
    public async Task BindRobotToUserTest()
    {
      var user = data.Users.First();
      var robot = data.Robots.First();
      robot.UserID = 0;
      await _manager.BindRobotToUser(robot, user);
      Assert.AreNotEqual(0, robot.UserID);
      Assert.AreEqual(robot.UserID, user.UserID);
    }

    [TestMethod]
    public void GetMyRobotsTest()
    {
      var user = data.Users.First();
      var robot = data.Robots.First();
      Assert.AreSame(robot, _manager.GetMyRobots(user).First());
    }
  }
}
