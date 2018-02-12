using ChoixResto.Models;
using ChoixResto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChoixResto.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult ShowRestaurantList()
        {
            List<Restaurant> Restaurants = new List<Restaurant>
                {
                    new Restaurant { Name = "Resto pinambour", PhoneNumber = "1234" },
                    new Restaurant { Name = "Resto tologie", PhoneNumber = "1234" },
                    new Restaurant { Name = "Resto ride", PhoneNumber = "5678" },
                    new Restaurant { Name = "Resto toro", PhoneNumber = "555" }
                };
                return PartialView(Restaurants);
            }
        }
}