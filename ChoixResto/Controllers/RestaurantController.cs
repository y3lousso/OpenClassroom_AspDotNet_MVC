using ChoixRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChoixRestaurant.Controllers
{
    public class RestaurantController : Controller
    {
        private IDal dal;

        public RestaurantController() : this(new Dal())
        {

        }

        public RestaurantController(IDal dalIoc)
        {
            dal = dalIoc;
        }

        public ActionResult Index()
        {
            List<Restaurant> listeDesRestaurants = dal.GetAllRestaurants();
            return View(listeDesRestaurants);
        }

        public ActionResult ModifyRestaurant(int? id)
        {
            if (id.HasValue)
            {
                Restaurant restaurant = dal.GetAllRestaurants().FirstOrDefault(r => r.Id == id.Value);
                if (restaurant == null)
                    return View("Error");
                return View(restaurant);
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        public ActionResult ModifyRestaurant(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return View(restaurant);
            }
            else
            {
                dal.ModifyRestaurant(restaurant.Id, restaurant.Name, restaurant.PhoneNumber, restaurant.Email);
                return RedirectToAction("Index");
            }
        }

        public ActionResult CreateRestaurant()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRestaurant(Restaurant restaurant)
        {
            if (dal.RestaurantExist(restaurant.Name))
            {
                ModelState.AddModelError("Name", "Ce nom de restaurant existe déjà");
                return View(restaurant);
            }
            if (!ModelState.IsValid)
            {
                return View(restaurant);
            }
            dal.CreateRestaurant(restaurant.Name, restaurant.PhoneNumber, restaurant.Email);
            return RedirectToAction("Index");
        }
    }
}