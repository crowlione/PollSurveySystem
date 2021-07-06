using PollSurveySystem.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollSurveySystem.Models.ViewModels
{
    public class PollDetailsViewModel
    {
        //public PollDetailsViewModel(int pollID, string question, IEnumerable<PollOption> pollOption)
        //{
        //    PollID = pollID;
        //    Question = question;
        //    PollOption = pollOption;
        //}

        public int PollID { get; set; }
       // public int PollOptionID { get; set; }
        //public string OptionValue { get; set; }
        public string Question { get; set; }
        public IEnumerable<DB.PollOption> PollOption { get; set; }
    }
}
