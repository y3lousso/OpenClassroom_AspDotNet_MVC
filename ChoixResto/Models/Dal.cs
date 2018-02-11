using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChoixResto.Models
{
    public class Dal : IDal
    {
        private MyDbContext db;

        public Dal()
        {
            db = new MyDbContext();
        }

        public void CreateRestaurant(string name, string phoneNumber)
        {
            db.Restaurants.Add(new Restaurant { Name = name, PhoneNumber = phoneNumber });
            db.SaveChanges();
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return db.Restaurants.ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        
    }
}