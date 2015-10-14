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
    public void Index_Contains_All_Users()
    {
      // Arrange - create the mock repository
      Mock<ICommonRepository<User>> mock = new Mock<ICommonRepository<User>>();
      mock.Setup(m => m.Data).Returns(new User[]
      {
        new User {UserID = 1, Name = "P1"},
        new User {UserID = 2, Name = "P2"},
        new User {UserID = 3, Name = "P3"},
      });
      // Arrange - create a controller
      AdminController target = new AdminController(mock.Object);
      // Action
      User[] result = ((IEnumerable<User>)target.Index().ViewData.Model).ToArray();
      // Assert
      Assert.AreEqual(result.Length, 3);
      Assert.AreEqual("P1", result[0].Name);
      Assert.AreEqual("P2", result[1].Name);
      Assert.AreEqual("P3", result[2].Name);
    }

    [TestMethod]
    public void Can_Edit_User()
    {
      // Arrange - create the mock repository
      Mock<ICommonRepository<User>> mock = new Mock<ICommonRepository<User>>();
      mock.Setup(m => m.Data).Returns(new User[]
      {
        new User {UserID = 1, Name = "P1"},
        new User {UserID = 2, Name = "P2"},
        new User {UserID = 3, Name = "P3"},
      });
      // Arrange - create the controller
      AdminController target = new AdminController(mock.Object);
      // Act
      User p1 = target.Edit(1).ViewData.Model as User;
      User p2 = target.Edit(2).ViewData.Model as User;
      User p3 = target.Edit(3).ViewData.Model as User;
      // Assert
      Assert.AreEqual(1, p1.UserID);
      Assert.AreEqual(2, p2.UserID);
      Assert.AreEqual(3, p3.UserID);
    }

    [TestMethod]
    public void Cannot_Edit_Nonexistent_User()
    {
      // Arrange - create the mock repository
      Mock<ICommonRepository<User>> mock = new Mock<ICommonRepository<User>>();
      mock.Setup(m => m.Data).Returns(new User[]
      {
        new User {UserID = 1, Name = "P1"},
        new User {UserID = 2, Name = "P2"},
        new User {UserID = 3, Name = "P3"},
      });
      // Arrange - create the controller
      AdminController target = new AdminController(mock.Object);
      // Act
      User result = (User)target.Edit(4).ViewData.Model;
      // Assert
      Assert.IsNull(result);
    }

    [TestMethod]
    public void Can_Save_Valid_Changes()
    {
      // Arrange - create mock repository
      Mock<ICommonRepository<User>> mock = new Mock<ICommonRepository<User>>();
      // Arrange - create the controller
      AdminController target = new AdminController(mock.Object);
      // Arrange - create a User
      User User = new User { Name = "Test" };
      // Act - try to save the User
      ActionResult result = target.Edit(User);
      // Assert - check that the repository was called
      mock.Verify(m => m.SaveData(User));
      // Assert - check the method result type
      Assert.IsNotInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod]
    public void Cannot_Save_Invalid_Changes()
    {
      // Arrange - create mock repository
      Mock<ICommonRepository<User>> mock = new Mock<ICommonRepository<User>>();
      // Arrange - create the controller
      AdminController target = new AdminController(mock.Object);
      // Arrange - create a User
      User User = new User { Name = "Test" };
      // Arrange - add an error to the model state
      target.ModelState.AddModelError("error", "error");
      // Act - try to save the User
      ActionResult result = target.Edit(User);
      // Assert - check that the repository was not called
      mock.Verify(m => m.SaveData(It.IsAny<User>()), Times.Never());
      // Assert - check the method result type
      Assert.IsInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod]
    public void Can_Delete_Valid_Users()
    {
      // Arrange - create a User
      User prod = new User { UserID = 2, Name = "Test" };
      // Arrange - create the mock repository
      Mock<ICommonRepository<User>> mock = new Mock<ICommonRepository<User>>();
      mock.Setup(m => m.Data).Returns(new User[]
      {
        new User {UserID = 1, Name = "P1"},
        prod,
        new User {UserID = 3, Name = "P3"},
      });
      // Arrange - create the controller
      AdminController target = new AdminController(mock.Object);
      // Act - delete the User
      target.Delete(prod.UserID);
      // Assert - ensure that the repository delete method was
      // called with the correct User
      mock.Verify(m => m.DeleteData(prod.UserID));
    }
  }
}
