using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ChoixResto.Models
{
    public class Dal : IDal
    {
        private MyDbContext db;

        public Dal()
        {
            db = new MyDbContext();
        }

        #region Restaurant
        public void CreateRestaurant(string name, string phoneNumber)
        {
            db.Restaurants.Add(new Restaurant { Name = name, PhoneNumber = phoneNumber });
            db.SaveChanges();
        }

        public void ModifyRestaurant(int id, string name, string phoneNumber)
        {
            Restaurant resto = db.Restaurants.FirstOrDefault(r => r.Id == id);
            if(resto != null)
            {
                resto.Name = name;
                resto.PhoneNumber = phoneNumber;
                db.SaveChanges();
            }
        }

        public bool RestaurantExist(string name)
        {
            return db.Restaurants.FirstOrDefault(r => r.Name == name) !=  null ? true : false;

        }

        public List<Restaurant> GetAllRestaurants()
        {
            return db.Restaurants.ToList();
        }
        #endregion

        #region User
        public User GetUser(int id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

       public User GetUser(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
                return GetUser(id);
            return null;
        }

        public int AddUser(string name, string password)
        {
            string encodedPassword = EncodeMD5(password);
            User user = new User { Name = name, Password = encodedPassword };
            db.Users.Add(user);
            db.SaveChanges();
            return user.Id;
        }

        public User Authentificate(string name, string password)
        {
            string encodedPassword = EncodeMD5(password);
            return db.Users.FirstOrDefault(u => u.Name == name && u.Password== encodedPassword);
        }
        #endregion

        #region Survey/Vote

        public bool HasAlreadyVoted(int idSurvey, string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                Survey survey = db.Surveys.First(s => s.Id == idSurvey);
                if (survey.Votes == null)
                {
                    return false;
                }
                else
                {
                    return survey.Votes.Any(v => v.User != null && v.User.Id == id);
                }          
            }
            else
            { 
                return false;
            }
        }

        public int CreateSurvey()
        {
            Survey survey = new Survey { Date = DateTime.Now};
            db.Surveys.Add(survey);
            db.SaveChanges();
            return survey.Id;
        }

        public void AddVote(int idSurvey, int idRestaurant, int idUser)
        {
            Vote vote = new Vote
            {
                Restaurant = db.Restaurants.First(r => r.Id == idRestaurant),
                User = db.Users.First(u => u.Id == idUser)
            };
            Survey survey = db.Surveys.First(s => s.Id == idSurvey);
            if(survey.Votes==null)
            {
                survey.Votes = new List<Vote>();
            }
            survey.Votes.Add(vote);
            db.SaveChanges();

        }

        public List<Result> GetResults(int idSurvey)
        {
            List<Result> results = new List<Result>();

            List<Restaurant> restaurants = GetAllRestaurants();
            Survey survey = db.Surveys.First(s => s.Id == idSurvey);
            foreach(IGrouping<int, Vote> grouping in survey.Votes.GroupBy(v=> v.Restaurant.Id))
            {
                int idRestaurant = grouping.Key;
                Restaurant restaurant = restaurants.First(r => r.Id == idRestaurant);
                int voteAmout = grouping.Count();
                results.Add(new Result { Name = restaurant.Name, PhoneNumber = restaurant.PhoneNumber, VoteAmount = voteAmout });
            }
            return results;        
        }

        #endregion

        public void Dispose()
        {
            db.Dispose();
        }

        private string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "ChoixResto" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }


    }
}