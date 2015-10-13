using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Robots.Domain.Abstract;
using Robots.Domain.Entities;
using Robots.WebUI.Controllers;
using Robots.WebUI.Infrastructure.Abstract;
using Robots.WebUI.Models;

namespace Robots.Tests
{
  [TestClass]
  public class AccountTests
  {
    [TestMethod]
    public void Can_Login_With_Valid_Credentials()
    {
      // Arrange - create a mock authentication provider
      Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
      mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);
      // Arrange - create the view model
      LoginViewModel model = new LoginViewModel
      {
        UserName = "admin",
        Password = "secret"
      };
      // Arrange - create the controller
      AccountController target = new AccountController(mock.Object, null);
      // Act - authenticate using valid credentials
      ActionResult result = target.Login(model,"/MyURL");
      // Assert
      Assert.IsInstanceOfType(result, typeof(RedirectResult));
      Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
    }

    [TestMethod]
    public void Cannot_Login_With_Invalid_Credentials()
    {
      // Arrange - create a mock authentication provider
      Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
      mock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);
      // Arrange - create the view model
      LoginViewModel model = new LoginViewModel
      {
        UserName = "badUser",
        Password = "badPass"
      };
      // Arrange - create the controller
      AccountController target = new AccountController(mock.Object, null);
      // Act - authenticate using valid credentials
      ActionResult result = target.Login(model, "/MyURL");
      // Assert
      Assert.IsInstanceOfType(result, typeof(ViewResult));
      Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
    }

    [TestMethod]
    public void Registration_adds_data()
    {

      // Arrange - create the mock repository
      Mock<IUserRepository> mock = new Mock<IUserRepository>();
      mock.Setup(m => m.Users).Returns(new User[]
      {
        new User {UserID = 1, Name = "P1"},
        new User {UserID = 2, Name = "P2"},
        new User {UserID = 3, Name = "P3"},
      });
      // Arrange - create a controller
      AccountController target = new AccountController(null, mock.Object);
      // Arrange - create a new user
      User user = new User() {Name = "11"};

      // Act - registration
      ActionResult result = target.SignUp(user);
      // Assert - check that the repository was called
      mock.Verify(m => m.SaveUser(user));
      // Assert - check the method result type
      Assert.IsNotInstanceOfType(result, typeof(ViewResult));
    }
  }
}
