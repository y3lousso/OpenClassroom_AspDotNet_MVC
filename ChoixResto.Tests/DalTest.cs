using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChoixResto.Models;
using System.Collections.Generic;

namespace ChoixResto.Tests
{
    [TestClass]
    public class DalTest
    {
        [TestMethod]
        public void CreateRestaurant_InputNewRestaurant_OuputGetAllRestaurantReturnIt()
        {
            using (IDal dal = new Dal())
            {
                dal.CreateRestaurant("MyRestaurant01", "0102030405");
                List<Restaurant> restaurants = dal.GetAllRestaurants();

                Assert.IsNotNull(restaurants);
                Assert.AreEqual(1, restaurants.Count);
                Assert.AreEqual("MyRestaurant01", restaurants[0].Name);
                Assert.AreEqual("0102030405", restaurants[0].PhoneNumber);
            }
        }
    }
}
