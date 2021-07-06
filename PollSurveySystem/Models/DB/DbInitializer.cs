using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollSurveySystem.Models.DB
{
    public static class DbInitializer
    {
        public static void Initialize(PollSystemContext context)
        {
            context.Database.EnsureCreated();

            // Look for any data.
            if (context.PollQuestions.Any())
            {
                return;   // DB has been seeded
            }

            var pq = new PollQuestion { Question = "Which is the largest country?", IsActive = true };
            context.PollQuestions.Add(pq);
            context.SaveChanges();

            var panswers = new PollOption[]
            {
                new PollOption{PollQuestionID=pq.PollQuestionID, OptionValue = "China", Votes = 0},
                new PollOption{PollQuestionID=pq.PollQuestionID, OptionValue = "Russia", Votes = 0},
                new PollOption{PollQuestionID=pq.PollQuestionID, OptionValue = "The USA", Votes = 0},
                new PollOption{PollQuestionID=pq.PollQuestionID, OptionValue = "Canada", Votes = 0}
            };
            foreach (PollOption po in panswers)
            {
                context.PollOptions.Add(po);
            }
            context.SaveChanges();
        }
    }
}
