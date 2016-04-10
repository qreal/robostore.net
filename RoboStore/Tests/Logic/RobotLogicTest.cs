using System.Linq;
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
    public void CreateRobotTest()
    {
      var amount = data.Robots.Data.Count();
      var result = _manager.CreateRobot();
      Assert.AreEqual(amount + 1, data.Robots.Data.Count());
      Assert.AreEqual(result.RobotID + result.ActivationCode, 0);
    }

    [TestMethod]
    public void GetRobotByActivationCodeTest()
    {
      var code = data.Robots.Data.First().ActivationCode;
      Assert.AreSame(data.Robots.Data.First(), _manager.GetRobotByActivationCode(code));
    }

    [TestMethod]
    public void GetRobotByIdTest()
    {
      var id = data.Robots.Data.First().RobotID;
      Assert.AreSame(data.Robots.Data.First(), _manager.GetRobotById(id));
    }

    [TestMethod]
    public void BindRobotToUserTest()
    {
      var user = data.Users.Data.First();
      var robot = data.Robots.Data.First();
      robot.UserID = 0;
      _manager.BindRobotToUser(robot, user);
      Assert.AreNotEqual(0, robot.UserID);
      Assert.AreEqual(robot.UserID, user.UserID);
    }

    [TestMethod]
    public void GetMyRobotsTest()
    {
      var user = data.Users.Data.First();
      var robot = data.Robots.Data.First();
      Assert.AreSame(robot, _manager.GetMyRobots(user).First());
    }

    [TestMethod]
    public void GetRobotByProgramRobotIdTest()
    {
      var robot = data.Robots.Data.First();
      var programRobot = data.ProgramRobots.Data.First();
      var result = _manager.GetRobotByProgramRobotId(programRobot.ProgramRobotID);
      Assert.AreSame(robot, result);
    }
  }
}
