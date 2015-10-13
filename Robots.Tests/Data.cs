using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Robots.Domain.Abstract;
using Robots.Domain.Entities;
using Robots.WebUI.Controllers;

namespace Robots.Tests
{
  [TestClass]
  public class Data
  {
    [TestMethod]
    public void Can_get_data_from_controller()
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
      // потому что хотим обратиться к приватному полю
      PrivateObject privateObject = new PrivateObject(target);

      // Action

      User[] result = ( ( (IUserRepository)privateObject.GetField("users")).Users ).ToArray();

      // Assert
      Assert.AreEqual(result.Length, 3);
      Assert.AreEqual("P1", result[0].Name);
      Assert.AreEqual("P2", result[1].Name);
      Assert.AreEqual("P3", result[2].Name);


    }
  }
}
