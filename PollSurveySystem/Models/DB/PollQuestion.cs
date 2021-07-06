using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollSurveySystem.Models.DB
{
    public class PollQuestion
    {
        public int PollQuestionID { get; set; }
        public string Question { get; set; }
        public bool IsActive { get; set; }
    }
}
