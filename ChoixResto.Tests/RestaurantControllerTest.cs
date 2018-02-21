using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ChoixRestaurant.Controllers;
using ChoixRestaurant.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChoixRestaurant.Tests
{
    [TestClass]
    public class RestaurantControllerTest
    {
        [TestMethod]
        public void RestaurantController_Index_LeControleurEstOk()
        {
            using (IDal dal = new DalForTest())
            {
                RestaurantController controller = new RestaurantController(dal);

                ViewResult resultat = (ViewResult)controller.Index();

                List<Restaurant> model = (List<Restaurant>)resultat.Model;
                Assert.AreEqual("Restaurant pinambour", model[0].Name);
            }
        }

        [TestMethod]
        public void RestaurantController_ModifierRestaurantAvecRestoInvalide_RenvoiVueParDefaut()
        {
            using (IDal dal = new DalForTest())
            {
                RestaurantController controller = new RestaurantController(dal);
                controller.ModelState.AddModelError("Nom", "Le nom du restaurant doit être saisi");

                ViewResult resultat = (ViewResult)controller.ModifyRestaurant(new Restaurant { Id = 1, Name = null, PhoneNumber = "0102030405", Email ="abc" });

                Assert.AreEqual(string.Empty, resultat.ViewName);
                Assert.IsFalse(resultat.ViewData.ModelState.IsValid);
            }
        }
    }
}
