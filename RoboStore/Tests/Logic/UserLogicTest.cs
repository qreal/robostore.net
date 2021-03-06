﻿using System.Linq;
using Domain;
using Domain.Entities;
using Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Logic
{
  [TestClass]
  public class UserLogicTest : LogicTest
  {
    private UserManager manager;

    public UserLogicTest()
    {
      manager = new UserManager(data);
    }

    [TestMethod]
    public void TryEnterLogicTest()
    {
      var user = data.Users.Data.First();
      Assert.AreSame(user, manager.FindUserByLoginPassword(user.Login, user.Password));
      Assert.AreEqual(null, manager.FindUserByLoginPassword(user.Login + MoqDataGenerator.GetRandomString(1), user.Password));
    }

    [TestMethod]
    public void CreateUserLogicTest()
    {
      var user = new User
      {
        Login = MoqDataGenerator.GetRandomString(10),
        Password = MoqDataGenerator.GetRandomString(10)
      };
      int amount = data.Users.Data.Count();
      manager.CreateUser(user.Login, user.Password);
      Assert.AreEqual(amount + 1, data.Users.Data.Count());
      Assert.AreEqual(user.Login, data.Users.Data.Last().Login);
      Assert.AreEqual(user.Password, data.Users.Data.Last().Password);
    }
  }
}
