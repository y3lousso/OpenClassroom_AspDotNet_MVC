using ChoixResto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChoixResto.ViewModels
{
    public class HomeViewModel
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public List<Restaurant> Restaurants { get; set; }
        public string Login { get; set; }
    }
}