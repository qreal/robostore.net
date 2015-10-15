using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Robots.Domain.Abstract;
using Robots.Domain.Entities;
using Robots.WebUI.Controllers;


namespace Robots.Tests
{
  [TestClass]
  public class AdminTests
  {
    [TestMethod]
    public void Index_Contains_All_Data()
    {
      // Arrange - create the mock repository of Users
      Mock<ICommonRepository<User>> mockU = new Mock<ICommonRepository<User>>();
      mockU.Setup(m => m.Data).Returns(new User[]
      {
        new User {UserID = 1, Name = "P1"},
        new User {UserID = 2, Name = "P2"},
        new User {UserID = 3, Name = "P3"},
      });
      // Arrange - create the mock repository of Robots
      Mock<ICommonRepository<Robot>> mockR = new Mock<ICommonRepository<Robot>>();
      mockR.Setup(m => m.Data).Returns(new Robot[]
      {
        new Robot {RobotID = 1, Name = "P1"},
        new Robot {RobotID = 2, Name = "P2"},
        new Robot {RobotID = 3, Name = "P3"},
      });
      // Arrange - create a controller
      AdminController target = new AdminController(mockU.Object, mockR.Object);
      // Action
      User[] resultU = ((IEnumerable<User>)target.Index().ViewData.Model).ToArray();
      Robot[] resultR = ((IEnumerable<Robot>)target.IndexRobot().ViewData.Model).ToArray();
      // Assert
      Assert.AreEqual(resultU.Length, 3);
      Assert.AreEqual("P1", resultU[0].Name);
      Assert.AreEqual("P2", resultU[1].Name);
      Assert.AreEqual("P3", resultU[2].Name);
      Assert.AreEqual(resultR.Length, 3);
      Assert.AreEqual("P1", resultR[0].Name);
      Assert.AreEqual("P2", resultR[1].Name);
      Assert.AreEqual("P3", resultR[2].Name);
    }

    [TestMethod]
    public void Can_Edit_Data()
    {
      // Arrange - create the mock repository of Users
      Mock<ICommonRepository<User>> mockU = new Mock<ICommonRepository<User>>();
      mockU.Setup(m => m.Data).Returns(new User[]
      {
        new User {UserID = 1, Name = "P1"},
        new User {UserID = 2, Name = "P2"},
        new User {UserID = 3, Name = "P3"},
      });
      // Arrange - create the mock repository of Robots
      Mock<ICommonRepository<Robot>> mockR = new Mock<ICommonRepository<Robot>>();
      mockR.Setup(m => m.Data).Returns(new Robot[]
      {
        new Robot {RobotID = 1, Name = "P1"},
        new Robot {RobotID = 2, Name = "P2"},
        new Robot {RobotID = 3, Name = "P3"},
      });
      // Arrange - create a controller
      AdminController target = new AdminController(mockU.Object, mockR.Object);
      // Act
      Robot robot1 = target.EditRobot(1).ViewData.Model as Robot;
      Robot robot2 = target.EditRobot(2).ViewData.Model as Robot;
      Robot robot3 = target.EditRobot(3).ViewData.Model as Robot;
      User user1 = target.EditUser(1).ViewData.Model as User;
      User user2 = target.EditUser(2).ViewData.Model as User;
      User user3 = target.EditUser(3).ViewData.Model as User;
      // Assert
      Assert.AreEqual(1, user1.UserID);
      Assert.AreEqual(2, user2.UserID);
      Assert.AreEqual(3, user3.UserID);
      Assert.AreEqual(1, robot1.RobotID);
      Assert.AreEqual(2, robot2.RobotID);
      Assert.AreEqual(3, robot3.RobotID);
    }

    [TestMethod]
    public void Cannot_Edit_Nonexistent_Data()
    {
      // Arrange - create the mock repository of Users
      Mock<ICommonRepository<User>> mockU = new Mock<ICommonRepository<User>>();
      mockU.Setup(m => m.Data).Returns(new User[]
      {
        new User {UserID = 1, Name = "P1"},
        new User {UserID = 2, Name = "P2"},
        new User {UserID = 3, Name = "P3"},
      });
      // Arrange - create the mock repository of Robots
      Mock<ICommonRepository<Robot>> mockR = new Mock<ICommonRepository<Robot>>();
      mockR.Setup(m => m.Data).Returns(new Robot[]
      {
        new Robot {RobotID = 1, Name = "P1"},
        new Robot {RobotID = 2, Name = "P2"},
        new Robot {RobotID = 3, Name = "P3"},
      });
      // Arrange - create the controller
      AdminController target = new AdminController(mockU.Object, mockR.Object);
      // Act
      User resultU = (User)target.EditUser(4).ViewData.Model;
      Robot resultR = (Robot) target.EditRobot(4).ViewData.Model;
      // Assert
      Assert.IsNull(resultU);
      Assert.IsNull(resultR);
    }

    [TestMethod]
    public void Can_Save_Valid_Changes_Data()
    {
      // Arrange - create mock repository
      Mock<ICommonRepository<User>> mockU = new Mock<ICommonRepository<User>>();
      Mock<ICommonRepository<Robot>> mockR = new Mock<ICommonRepository<Robot>>();
      // Arrange - create the controller
      AdminController target = new AdminController(mockU.Object, mockR.Object);
      // Arrange - create a User and a Robot
      User user = new User { Name = "Test" };
      Robot robot = new Robot() {Name = "Roboto"};
      // Act - try to save the User and the Robot
      ActionResult resultU = target.EditUser(user);
      ActionResult resultR = target.EditRobot(robot);
      // Assert - check that the repository was called
      mockU.Verify(m => m.SaveData(user));
      mockR.Verify(m => m.SaveData(robot));
      // Assert - check the method result type
      Assert.IsNotInstanceOfType(resultU, typeof(ViewResult));
      Assert.IsNotInstanceOfType(resultR, typeof(ViewResult));
    }

    [TestMethod]
    public void Cannot_Save_Invalid_Changes_Data()
    {
      // Arrange - create mock repository
      Mock<ICommonRepository<User>> mockU = new Mock<ICommonRepository<User>>();
      Mock<ICommonRepository<Robot>> mockR = new Mock<ICommonRepository<Robot>>();
      // Arrange - create the controller
      AdminController target = new AdminController(mockU.Object, mockR.Object);
      // Arrange - create a User and a Robot
      User user = new User { Name = "Test" };
      Robot robot = new Robot() {Name = "Roboto"};
      // Arrange - add an error to the model state
      target.ModelState.AddModelError("error", "error");
      // Act - try to save the User and the Robot
      ActionResult resultU = target.EditUser(user);
      ActionResult resultR = target.EditRobot(robot);
      // Assert - check that the repository was not called
      mockU.Verify(m => m.SaveData(It.IsAny<User>()), Times.Never());
      mockR.Verify(m => m.SaveData(It.IsAny<Robot>()), Times.Never());
      // Assert - check the method result type
      Assert.IsInstanceOfType(resultU, typeof(ViewResult));
      Assert.IsInstanceOfType(resultR, typeof(ViewResult));
    }

    [TestMethod]
    public void Can_Delete_Valid_Data()
    {
      // Arrange - create a User and a Robot
      User user = new User { UserID = 2, Name = "Test" };
      Robot robot = new Robot {RobotID = 2, Name = "Test"};
      // Arrange - create the mock repository
      Mock<ICommonRepository<User>> mockU = new Mock<ICommonRepository<User>>();
      Mock<ICommonRepository<Robot>> mockR = new Mock<ICommonRepository<Robot>>();
      mockU.Setup(m => m.Data).Returns(new User[]
      {
        new User {UserID = 1, Name = "P1"},
        user,
        new User {UserID = 3, Name = "P3"},
      });
      mockR.Setup(m => m.Data).Returns(new Robot[]
      {
        new Robot {RobotID = 1, Name = "P1"},
        robot,
        new Robot {RobotID = 3, Name = "P3"},
      });
      // Arrange - create the controller
      AdminController target = new AdminController(mockU.Object, mockR.Object);
      // Act - delete the User and the Robot
      target.DeleteUser(user.UserID);
      target.DeleteRobot(robot.RobotID);
      // Assert - ensure that the repository delete method was
      // called with the correct User and Robot
      mockU.Verify(m => m.DeleteData(user.UserID));
      mockU.Verify(m => m.DeleteData(robot.RobotID));
    }
  }
}
