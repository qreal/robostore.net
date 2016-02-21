using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Store.Models.Data;
using Store.Models.Entities;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
      public static List<string> Messages = new List<string>();

    // GET: Home
    public string Index()
    {
      return "hi";
    }
  }
}