using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChoixResto.ViewModels
{
    public class RestaurantCheckBoxViewModel
    {
        public int Id { get; set; }
        public string NameAndPhoneNumber { get; set; }
        public bool IsChecked { get; set; }

    }
}