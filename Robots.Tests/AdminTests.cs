﻿using System.Collections.Generic;
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
      //Robot[] resultR = ((IEnumerable<Robot>)target.Index().ViewData.Model).ToArray();
      // Assert
      Assert.AreEqual(resultU.Length, 3);
      Assert.AreEqual("P1", resultU[0].Name);
      Assert.AreEqual("P2", resultU[1].Name);
      Assert.AreEqual("P3", resultU[2].Name);
      //Assert.AreEqual(resultR.Length, 3);
      //Assert.AreEqual("P1", resultR[0].Name);
      //Assert.AreEqual("P2", resultR[1].Name);
      //Assert.AreEqual("P3", resultR[2].Name);
    }

    [TestMethod]
    public void Can_Edit_User()
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
      //Robot robot1 = target.EditUser(1).ViewData.Model as Robot;
      //Robot robot2 = target.EditUser(2).ViewData.Model as Robot;
      //Robot robot3 = target.EditUser(3).ViewData.Model as Robot;
      User user1 = target.EditUser(1).ViewData.Model as User;
      User user2 = target.EditUser(2).ViewData.Model as User;
      User user3 = target.EditUser(3).ViewData.Model as User;
      // Assert
      Assert.AreEqual(1, user1.UserID);
      Assert.AreEqual(2, user2.UserID);
      Assert.AreEqual(3, user3.UserID);
    }

    [TestMethod]
    public void Cannot_Edit_Nonexistent_User()
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
      User result = (User)target.EditUser(4).ViewData.Model;
      // Assert
      Assert.IsNull(result);
    }

    [TestMethod]
    public void Can_Save_Valid_Changes()
    {
      // Arrange - create mock repository
      Mock<ICommonRepository<User>> mockU = new Mock<ICommonRepository<User>>();
      Mock<ICommonRepository<Robot>> mockR = new Mock<ICommonRepository<Robot>>();
      // Arrange - create the controller
      AdminController target = new AdminController(mockU.Object, mockR.Object);
      // Arrange - create a User
      User user = new User { Name = "Test" };
      // Act - try to save the User
      ActionResult result = target.EditUser(user);
      // Assert - check that the repository was called
      mockU.Verify(m => m.SaveData(user));
      // Assert - check the method result type
      Assert.IsNotInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod]
    public void Cannot_Save_Invalid_Changes()
    {
      // Arrange - create mock repository
      Mock<ICommonRepository<User>> mockU = new Mock<ICommonRepository<User>>();
      Mock<ICommonRepository<Robot>> mockR = new Mock<ICommonRepository<Robot>>();
      // Arrange - create the controller
      AdminController target = new AdminController(mockU.Object, mockR.Object);
      // Arrange - create a User
      User user = new User { Name = "Test" };
      // Arrange - add an error to the model state
      target.ModelState.AddModelError("error", "error");
      // Act - try to save the User
      ActionResult result = target.EditUser(user);
      // Assert - check that the repository was not called
      mockU.Verify(m => m.SaveData(It.IsAny<User>()), Times.Never());
      // Assert - check the method result type
      Assert.IsInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod]
    public void Can_Delete_Valid_Users()
    {
      // Arrange - create a User
      User prod = new User { UserID = 2, Name = "Test" };
      // Arrange - create the mock repository
      Mock<ICommonRepository<User>> mockU = new Mock<ICommonRepository<User>>();
      Mock<ICommonRepository<Robot>> mockR = new Mock<ICommonRepository<Robot>>();
      mockU.Setup(m => m.Data).Returns(new User[]
      {
        new User {UserID = 1, Name = "P1"},
        prod,
        new User {UserID = 3, Name = "P3"},
      });
      // Arrange - create the controller
      AdminController target = new AdminController(mockU.Object, mockR.Object);
      // Act - delete the User
      target.DeleteUser(prod.UserID);
      // Assert - ensure that the repository delete method was
      // called with the correct User
      mockU.Verify(m => m.DeleteData(prod.UserID));
    }
  }
}
