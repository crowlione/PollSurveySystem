using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollSurveySystem.Models.ViewModels
{
    public class UpdateVotesViewModel
    {
        public int PollID { get; set; }
        public int PollOptionID { get; set; }
    }
}
