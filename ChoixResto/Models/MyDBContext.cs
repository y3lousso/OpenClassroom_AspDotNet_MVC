using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ChoixRestaurant.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<User> Users { get; set; }

    }
}