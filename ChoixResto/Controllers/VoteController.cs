using ChoixRestaurant.Models;
using ChoixResto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChoixResto.Controllers
{
    public class VoteController : Controller
    {
        private IDal dal;

        public VoteController() : this(new Dal())
        {

        }

        public VoteController(IDal dalIoc)
        {
            dal = dalIoc;
        }

        // GET: Vote
        public ActionResult Index(int id)
        {
            RestaurantVoteViewModel viewModel = new RestaurantVoteViewModel
            {
                RestaurantsList = dal.GetAllRestaurants().Select(r => new RestaurantCheckBoxViewModel { Id = r.Id, NameAndPhoneNumber = string.Format("{0} ({1})", r.Name, r.PhoneNumber) }).ToList()
            };
            if (dal.HasAlreadyVoted(id, Request.Browser.Browser))
            {
                return RedirectToAction("ShowResult", new { id = id });
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(RestaurantVoteViewModel viewModel, int id)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            User utilisateur = dal.GetUser(Request.Browser.Browser);
            if (utilisateur == null)
                return new HttpUnauthorizedResult();
            foreach (RestaurantCheckBoxViewModel restaurantCheckBoxViewModel in viewModel.RestaurantsList.Where(r => r.IsChecked))
            {
                dal.AddVote(id, restaurantCheckBoxViewModel.Id, utilisateur.Id);
            }
            return RedirectToAction("ShowResult", new { id = id });
        }

        public ActionResult ShowResult(int id)
        {
            if (!dal.HasAlreadyVoted(id, Request.Browser.Browser))
            {
                return RedirectToAction("Index", new { id = id });
            }
            List<Result> resultats = dal.GetResults(id);
            return View(resultats.OrderByDescending(r => r.VoteAmount).ToList());
        }
    }
}