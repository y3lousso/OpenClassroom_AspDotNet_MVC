using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoixResto.Models
{
    public interface IDal : IDisposable
    {
        void CreateRestaurant(string name, string phoneNumber);
        List<Restaurant> GetAllRestaurants();
    }
}
