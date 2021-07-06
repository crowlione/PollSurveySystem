using Microsoft.AspNetCore.Http;
using PollSurveySystem.Models.DB;
using PollSurveySystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollSurveySystem.Models
{
    public class PollManager : IPollManager
    {
        private readonly PollSystemContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PollManager(PollSystemContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public PollDetailsViewModel ViewCurrentPoll(int Id)
        {
            if (_db.PollQuestions.Any())
            {
                var selectedPoll = _db.PollQuestions.Where(q => q.PollQuestionID == Id).FirstOrDefault();

                int pollId = selectedPoll.PollQuestionID;
                string Qestion = selectedPoll.Question;

                return new PollDetailsViewModel
                {
                    PollID = selectedPoll.PollQuestionID,
                    Question = selectedPoll.Question,
                    PollOption = _db.PollOptions.Where(po => po.PollQuestionID == selectedPoll.PollQuestionID).ToList()
                };
            }
            return null;
        }
        public IEnumerable<AllPollsViewModel> GetActivePolls()
        {
            if (_db.PollQuestions.Any())
            {
                return _db.PollQuestions.Where(q => q.IsActive == true).Select(q => new AllPollsViewModel
                {
                    PollID = q.PollQuestionID,
                    Question = q.Question,
                });
            }
            return Enumerable.Empty<AllPollsViewModel>();
        }

        public void SubmitVote(int PollID, int PollOption)
        {
            using (var dbContextTransaction = _db.Database.BeginTransaction())
            {
                try
                {
                    PollOption optionVotesToIncrement = _db.PollOptions.Where(o => o.PollQuestionID == PollID
                    && o.PollOptionID == PollOption).FirstOrDefault();
                    optionVotesToIncrement.Votes = optionVotesToIncrement.Votes + 1;
                    _db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch
                {
                    dbContextTransaction.Rollback();
                }
            }

            string cookieKey = "hasVoted" + PollID;
            string cookieValue = "has voted for" + PollID;
            
            CookieOptions hasVoted = new CookieOptions();
            hasVoted.Expires = DateTime.Now.AddHours(2);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieKey, cookieValue, hasVoted);
        }

        public bool AddPoll(AddPollViewModel pollModel)
        {
            using (var dbContextTransaction = _db.Database.BeginTransaction())
            {
                try
                { 
                    var answers = pollModel.Answer.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    PollQuestion poll = new PollQuestion();
                    poll.Question = pollModel.Question;
                    poll.IsActive = true;
                    _db.PollQuestions.Add(poll);
                    _db.SaveChanges();

                    foreach (var answer in answers)
                    {
                        PollOption option = new PollOption();
                        option.PollQuestionID = poll.PollQuestionID;
                        option.OptionValue = answer;
                        option.Votes = 0;
                        _db.PollOptions.Add(option);
                        _db.SaveChanges();
                    }
                    dbContextTransaction.Commit();
                }
                catch
                {
                    dbContextTransaction.Rollback();
                }
            }
            return true;
        }
    }
}
