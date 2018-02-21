using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace ChoixRestaurant.Models
{
    public class DalForTest : IDal
    {
        private List<Restaurant> listeDesRestaurants;
        private List<User> listeDesUsers;
        private List<Survey> listeDessondages;

        public DalForTest()
        {
            listeDesRestaurants = new List<Restaurant>
        {
            new Restaurant { Id = 1, Name = "Restaurant pinambour", PhoneNumber = "0102030405"},
            new Restaurant { Id = 2, Name = "Restaurant pinière", PhoneNumber = "0102030405"},
            new Restaurant { Id = 3, Name = "Restaurant toro", PhoneNumber = "0102030405"},
        };
            listeDesUsers = new List<User>();
            listeDessondages = new List<Survey>();
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return listeDesRestaurants;
        }

        public void CreateRestaurant(string nom, string telephone, string email)
        {
            int id = listeDesRestaurants.Count == 0 ? 1 : listeDesRestaurants.Max(r => r.Id) + 1;
            listeDesRestaurants.Add(new Restaurant { Id = id, Name = nom, PhoneNumber = telephone, Email = email });
        }

        public void ModifyRestaurant(int id, string nom, string telephone, string email)
        {
            Restaurant resto = listeDesRestaurants.FirstOrDefault(r => r.Id == id);
            if (resto != null)
            {
                resto.Name = nom;
                resto.PhoneNumber = telephone;
                resto.Email = email;
            }
        }

        public bool RestaurantExist(string nom)
        {
            return listeDesRestaurants.Any(resto => string.Compare(resto.Name, nom, StringComparison.CurrentCultureIgnoreCase) == 0);
        }

        public int AddUser(string nom, string motDePasse)
        {
            int id = listeDesUsers.Count == 0 ? 1 : listeDesUsers.Max(u => u.Id) + 1;
            listeDesUsers.Add(new User { Id = id, Name = nom, Password = motDePasse });
            return id;
        }

        public User Authentificate(string nom, string motDePasse)
        {
            return listeDesUsers.FirstOrDefault(u => u.Name == nom && u.Password == motDePasse);
        }

        public User GetUser(int id)
        {
            return listeDesUsers.FirstOrDefault(u => u.Id == id);
        }

        public User GetUser(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
                return GetUser(id);
            return null;
        }

        public int CreateSurvey()
        {
            int id = listeDessondages.Count == 0 ? 1 : listeDessondages.Max(s => s.Id) + 1;
            listeDessondages.Add(new Survey { Id = id, Date = DateTime.Now, Votes = new List<Vote>() });
            return id;
        }

        public void AddVote(int idSurvey, int idRestaurant, int idUser)
        {
            Vote vote = new Vote
            {
                Restaurant = listeDesRestaurants.First(r => r.Id == idRestaurant),
                User = listeDesUsers.First(u => u.Id == idUser)
            };
            Survey sondage = listeDessondages.First(s => s.Id == idSurvey);
            sondage.Votes.Add(vote);
        }

        public bool HasAlreadyVoted(int idSurvey, string idStr)
        {
            User utilisateur = GetUser(idStr);
            if (utilisateur == null)
                return false;
            Survey sondage = listeDessondages.First(s => s.Id == idSurvey);
            return sondage.Votes.Any(v => v.User.Id == utilisateur.Id);
        }

        public List<Result> GetResults(int idSurvey)
        {
            List<Restaurant> restaurants = GetAllRestaurants();
            List<Result> resultats = new List<Result>();
            Survey sondage = listeDessondages.First(s => s.Id == idSurvey);
            foreach (IGrouping<int, Vote> grouping in sondage.Votes.GroupBy(v => v.Restaurant.Id))
            {
                int idRestaurant = grouping.Key;
                Restaurant resto = restaurants.First(r => r.Id == idRestaurant);
                int nombreDeVotes = grouping.Count();
                resultats.Add(new Result { Name = resto.Name, PhoneNumber = resto.PhoneNumber, VoteAmount = nombreDeVotes });
            }
            return resultats;
        }

        public void Dispose()
        {
            listeDesRestaurants = new List<Restaurant>();
            listeDesUsers = new List<User>();
            listeDessondages = new List<Survey>();
        }
    }
}