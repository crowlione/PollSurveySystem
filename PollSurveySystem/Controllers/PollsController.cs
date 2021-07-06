using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PollSurveySystem.Models;
using PollSurveySystem.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollSurveySystem.Controllers
{
    public class PollsController : Controller
    {
        private readonly IPollManager _pollManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PollsController(IPollManager pollManager, IHttpContextAccessor httpContextAccessor)
        {
            _pollManager = pollManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SubmitVote(int PollID, int PollOption)
        {
            string cookieKey = "hasVoted" + PollID;
            string cookieValue = _httpContextAccessor.HttpContext.Request.Cookies[cookieKey];
            if (PollOption==0)
            {
                return View(_pollManager.ViewCurrentPoll(PollID));
            }
            else
            {
                if (cookieValue != null)
                {
                    if (cookieValue.Equals("has voted for" + PollID))
                    {
                        { ViewBag.Message = "You have already voted"; }
                        return View(_pollManager.ViewCurrentPoll(PollID));
                    }
                }
                _pollManager.SubmitVote(PollID, PollOption);
                return View(_pollManager.ViewCurrentPoll(PollID));
            }          
        }
    }
}
