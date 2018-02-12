using ChoixResto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChoixResto.Controllers
{
    public class RestaurantController : Controller
    {
        public ActionResult Index()
        {
            using (IDal dal = new Dal())
            {
                List<Restaurant> restaurants = dal.GetAllRestaurants();
                return View(restaurants);
            }
        }

        public ActionResult ModifierRestaurant(int? id)
        {
            if (id.HasValue)
            {
                using (IDal dal = new Dal())
                {
                    if (Request.HttpMethod == "POST")
                    {
                        string name = Request.Form["Name"];
                        string phoneNumber = Request.Form["PhoneNumber"];
                        dal.ModifyRestaurant(id.Value, name, phoneNumber);
                    }

                    Restaurant restaurant = dal.GetAllRestaurants().FirstOrDefault(r => r.Id == id.Value);
                    if (restaurant == null)
                        return View("Error");
                    return View(restaurant);
                }
            }
            else
                return View("Error");
        }

        [HttpPost]
        public ActionResult ModifierRestaurant(Restaurant restaurant)
        {
            using (IDal dal = new Dal())
            {
                dal.ModifyRestaurant(restaurant.Id, restaurant.Name, restaurant.PhoneNumber);
                return RedirectToAction("Index");
            }
        }
    }
}