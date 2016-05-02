using System.Linq;
using Domain.Managers;
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
    public void CreateRobotLogicTest()
    {
      var amount = data.Robots.Data.Count();
      var result = _manager.CreateRobot();
      Assert.AreEqual(amount + 1, data.Robots.Data.Count());
      Assert.AreEqual(result.RobotID + result.ActivationCode, 0);
    }

    [TestMethod]
    public void GetRobotByActivationCodeLogicTest()
    {
      var code = data.Robots.Data.First().ActivationCode;
      Assert.AreSame(data.Robots.Data.First(), _manager.GetRobotByActivationCode(code));
    }

    [TestMethod]
    public void GetRobotByIdLogicTest()
    {
      var id = data.Robots.Data.First().RobotID;
      Assert.AreSame(data.Robots.Data.First(), _manager.GetRobotById(id));
    }

    [TestMethod]
    public void BindRobotToUserLogicTest()
    {
      var user = data.Users.Data.First();
      var robot = data.Robots.Data.First();
      robot.UserID = 0;
      _manager.BindRobotToUser(robot, user);
      Assert.AreNotEqual(0, robot.UserID);
      Assert.AreEqual(robot.UserID, user.UserID);
    }

    [TestMethod]
    public void GetMyRobotsLogicTest()
    {
      var user = data.Users.Data.First();
      var robot = data.Robots.Data.First();
      Assert.AreSame(robot, _manager.GetRobotsByUser(user).First());
    }

    [TestMethod]
    public void GetRobotByProgramRobotIdLogicTest()
    {
      var robot = data.Robots.Data.First();
      var programRobot = data.ProgramRobots.Data.First();
      var result = _manager.GetRobotByProgramRobotId(programRobot.ProgramRobotID);
      Assert.AreSame(robot, result);
    }
  }
}
