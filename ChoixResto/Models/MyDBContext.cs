using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ChoixResto.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Survey> Sondages { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

    }
}