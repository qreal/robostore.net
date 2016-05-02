using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Data;
using Domain.Users;
using Store.Models.User;
using Store.Services;

namespace Store.Controllers
{
    public class UserController : Controller
    {
        private UserManager userManager;

        public UserController(IData d)
        {
            userManager = new UserManager(d);
        }

        // GET: User
        public ActionResult Registration()
            => View();

        [HttpPost]
        public ActionResult Registration(UserProfile profile)
        {
            // если пароль и логин введены
            if (ModelState.IsValid && userManager.FindUserByLoginPassword(profile.Login, profile.Password) == null)
            {
                userManager.CreateUser(profile.Login, profile.Password);
                return View("Welcome", (object) profile.Login);
            }
            return View();
        }

        public ActionResult Entrance()
        {
            ///
            /// todo убрать заглушку
            /// 
            //return View();
            var foundUser = userManager.FindUserByLoginPassword("Philip J. Fry", "11");
            FakeSession.User = foundUser;
            FakeSession.RobotIds = new List<int>();
            FakeSession.RobotIds.AddRange(foundUser.Robots.Select(x => x.RobotID));
            return RedirectToAction("ShowAllPrograms", "Program");
        }

        [HttpPost]
        public ActionResult Entrance(UserProfile profile)
        {
            var foundUser = userManager.FindUserByLoginPassword(profile.Login, profile.Password);
            if (ModelState.IsValid && foundUser != null)
            {
                FakeSession.User = foundUser;
                FakeSession.RobotIds = new List<int>();
                FakeSession.RobotIds.AddRange(foundUser.Robots.Select(x => x.RobotID));
                return RedirectToAction("ShowAllPrograms", "Program");
            }
            return View();
        }
    }
}