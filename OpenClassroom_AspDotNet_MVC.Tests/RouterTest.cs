using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

namespace OpenClassroom_AspDotNet_MVC.Tests
{
    [TestClass]
    public class RouterTest
    {
        private static RouteData DefinirUrl(string url)
        {
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns(url);
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            RouteData routeData = routes.GetRouteData(mockContext.Object);
            return routeData;
        }

        [TestMethod]
        public void Routes_PageHome_RetourneControleurHomeEtMethodeIndex()
        {
            RouteData routeData = DefinirUrl("~/");
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);
        }

        [TestMethod]
        public void Routes_PageHomeIndex2_RetourneControleurHomeEtMethodeIndexEtParam2()
        {
            RouteData routeData = DefinirUrl("~/Home/Index/2");
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
            Assert.AreEqual("2", routeData.Values["id"]);
        }

        [TestMethod]
        public void Routes_MeteoAujourdhui_RetourneControleurMeteoMethodeAfficherEtParametreAujourdhui()
        {
            DateTime aujourdhui = DateTime.Now;
            RouteData routeData = DefinirUrl(string.Format("~/{0}/{1}/{2}", aujourdhui.Day, aujourdhui.Month, aujourdhui.Year));
            Assert.IsNotNull(routeData);
            Assert.AreEqual("Meteo", routeData.Values["controller"]);
            Assert.AreEqual("Afficher", routeData.Values["action"]);
            Assert.AreEqual(aujourdhui.Day.ToString(), routeData.Values["jour"]);
            Assert.AreEqual(aujourdhui.Month.ToString(), routeData.Values["mois"]);
            Assert.AreEqual(aujourdhui.Year.ToString(), routeData.Values["annee"]);
        }

        [TestMethod]
        public void Routes_UrlBidon_RetourneNull()
        {
            RouteData routeData = DefinirUrl("/Url/bidon/pas/bonne");
            Assert.IsNull(routeData);
        }
    }
}
