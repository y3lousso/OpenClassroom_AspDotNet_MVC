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
        private IDal dal;

        public HomeController() : this(new Dal())
        {
        }

        public HomeController(IDal dalIoc)
        {
            dal = dalIoc;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost()
        {
            int idSondage = dal.CreateSurvey();
            return RedirectToAction("Index", "Vote", new { id = idSondage });
        }

    }
}