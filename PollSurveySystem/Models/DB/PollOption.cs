using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollSurveySystem.Models.DB
{
    public class PollOption
    {
        public int PollOptionID { get; set; }
        public int PollQuestionID { get; set; }
        public string OptionValue { get; set; }
        public int Votes { get; set; }
    }
}
