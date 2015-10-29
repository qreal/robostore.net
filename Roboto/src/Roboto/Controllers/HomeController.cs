using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Roboto.Models;
using Roboto.Models.Entities;

namespace Roboto.Controllers
{
    public class HomeController : Controller
    {
      private DataContext data;

      public HomeController(DataContext d)
      {
        data = d;
      }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            data.Users.Add(new User() {Name = "Vlad", Password = "USSR"});
            data.SaveChanges();
            return View();
        }

        public IActionResult DiagramConstructor()
        {
            ViewData["Message"] = "Your contact page.";
            
            return View(data.Users.ToList());
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
