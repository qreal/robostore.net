using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Services;

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
    public void TryEnterTest()
    {
      var user = data.Users.First();
      Assert.AreEqual(true, manager.TryEnter(user.Login, user.Password));
      Assert.AreEqual(false, manager.TryEnter(user.Login + MoqDataGenerator.GetRandomString(1), user.Password));
    }

    [TestMethod]
    public async Task CreateUserTest()
    {
      var user = new User
      {
        Login = MoqDataGenerator.GetRandomString(10),
        Password = MoqDataGenerator.GetRandomString(10)
      };
      int amount = data.Users.Count();
      await manager.CreateUser(user.Login, user.Password);
      Assert.AreEqual(amount + 1, data.Users.Count());
      Assert.AreEqual(user.Login, data.Users.Last().Login);
      Assert.AreEqual(user.Password, data.Users.Last().Password);
    }
  }
}
