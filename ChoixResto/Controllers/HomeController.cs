using ChoixRestaurant.Models;
using ChoixRestaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChoixRestaurant.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowDate(string id)
        {
            ViewBag.Message = "Bonjour " + id + " !";
            ViewData["Date"] = new DateTime(2012, 4, 28);
            return View("Index");
        }

    }
}