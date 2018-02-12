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
        void ModifyRestaurant(int id, string name, string phoneNumber);
        bool RestaurantExist(string name);
        List<Restaurant> GetAllRestaurants();

        User GetUser(int id);
        User GetUser(string id);
        int AddUser(string name, string password);
        User Authentificate(string Name, string password);

        bool HasAlreadyVoted(int id, string name);
        int CreateSurvey();
        void AddVote(int idSurvey, int tmp, int idUser);
        List<Result> GetResults(int idSurvey);

        
    }
}
