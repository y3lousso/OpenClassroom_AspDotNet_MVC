using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChoixRestaurant.Models
{
    public class InitChoixRestaurant : DropCreateDatabaseAlways<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {
            context.Restaurants.Add(new Restaurant { Id = 1, Name = "Restaurant pinambour", PhoneNumber = "0102030405", Email = "abc" });
            context.Restaurants.Add(new Restaurant { Id = 2, Name = "Restaurant pinière", PhoneNumber = "0102030406", Email = "abc" });
            context.Restaurants.Add(new Restaurant { Id = 3, Name = "Restaurant toro", PhoneNumber = "0102030407", Email = "abc" });

            base.Seed(context);
        }
    }
}