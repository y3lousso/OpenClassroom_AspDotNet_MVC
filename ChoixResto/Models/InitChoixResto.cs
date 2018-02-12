using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChoixResto.Models
{
    public class InitChoixResto : DropCreateDatabaseAlways<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {
            context.Restaurants.Add(new Restaurant { Id = 1, Name = "Resto pinambour", PhoneNumber = "123" });
            context.Restaurants.Add(new Restaurant { Id = 2, Name = "Resto pinière", PhoneNumber = "456" });
            context.Restaurants.Add(new Restaurant { Id = 3, Name = "Resto toro", PhoneNumber = "789" });

            base.Seed(context);
        }
    }
}