using System;
using ChoixRestaurant.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace ChoixRestaurant.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void HomeController_Index_RenvoiVueParDefaut()
        {
            HomeController controller = new HomeController();

            ViewResult resultat = (ViewResult)controller.Index();

            Assert.AreEqual(string.Empty, resultat.ViewName);
        }

        [TestMethod]
        public void HomeController_ShowDate_RenvoiVueIndexEtViewData()
        {
            HomeController controller = new HomeController();

            ViewResult resultat = (ViewResult)controller.ShowDate("Nicolas");

            Assert.AreEqual("Index", resultat.ViewName);
            Assert.AreEqual(new DateTime(2012, 4, 28), resultat.ViewData["date"]);
            Assert.AreEqual("Bonjour Nicolas !", resultat.ViewBag.Message);
        }
    }
}
