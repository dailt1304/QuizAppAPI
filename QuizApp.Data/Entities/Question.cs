using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Domain.Entities
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int QuizId { get; set; }
        public string? QuestionText { get; set; }
        public Quiz Quiz { get; set; } = null!;
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
        public ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
    }
}
