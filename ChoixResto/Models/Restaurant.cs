using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChoixRestaurant.Models
{
    [Table("Restaurants")]
    public class Restaurant
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom du restaurant doit être saisi")]
        public string Name { get; set; }

        [AtLeastOneOfTwo(Parametre1 = "PhoneNumber", Parametre2 = "Email", ErrorMessage = "Vous devez saisir au moins un moyen de contacter le restaurant")]
        public string PhoneNumber { get; set; }

        [AtLeastOneOfTwo(Parametre1 = "PhoneNumber", Parametre2 = "Email", ErrorMessage = "Vous devez saisir au moins un moyen de contacter le restaurant")]
        public string Email { get; set; }


    }
}