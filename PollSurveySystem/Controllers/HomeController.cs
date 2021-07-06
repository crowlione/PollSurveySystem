using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PollSurveySystem.Models;
using PollSurveySystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PollSurveySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPollManager _pollManager;

        public HomeController(IPollManager pollManager)
        {
            _pollManager = pollManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewCurrentPoll(int Id)
        {
            return View(_pollManager.ViewCurrentPoll(Id));
        }

        [HttpGet]
        public IActionResult GetActivePolls()
        {
            IEnumerable<AllPollsViewModel> polls = _pollManager.GetActivePolls();
            return View(polls.ToList());
        }

        public IActionResult AddPoll()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPoll(AddPollViewModel poll)
        {
            if (ModelState.IsValid)
            {
                if (_pollManager.AddPoll(poll))
                { ViewBag.Message = "Poll added successfully!"; }
                else return NotFound();
            }
            ModelState.Clear();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
