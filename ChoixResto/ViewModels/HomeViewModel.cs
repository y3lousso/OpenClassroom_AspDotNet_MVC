using ChoixRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChoixRestaurant.ViewModels
{
    public class HomeViewModel
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public List<Restaurant> Restaurants { get; set; }
        public string Login { get; set; }
    }
}