using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PollSurveySystem.Models.ViewModels
{
    public class AddPollViewModel
    {
        [Required(ErrorMessage = "Question is required.")]
        public string Question { get; set; }
        [Required(ErrorMessage = "Answer is required.")]
        public string Answer { get; set; }
    }
}
