using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChoixResto.ViewModels
{
    public class RestaurantVoteViewModel : IValidatableObject
    {

        public List<RestaurantCheckBoxViewModel> RestaurantsList { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!RestaurantsList.Any(r => r.IsChecked))
            {
                yield return new ValidationResult("Vous devez choisir au moins un restaurant.", new[] { "RestaurantsList" });
            }
        }
    }
}