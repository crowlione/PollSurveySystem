using PollSurveySystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollSurveySystem.Models
{
    public interface IPollManager
    {
        bool AddPoll(AddPollViewModel pollModel);
        IEnumerable<AllPollsViewModel> GetActivePolls();
        public PollDetailsViewModel ViewCurrentPoll(int Id);
        public void SubmitVote(int PollID, int PollOption);
    }
}
