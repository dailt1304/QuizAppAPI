using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Domain.Entities
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public string? Title { get; set; }
        public int TotalQuestions { get; set; }
        public int PassingScore { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();
        public bool EvaluateQuizResult(int correctAnswers)
        {
            var scorePercentage = (double)correctAnswers / TotalQuestions * 100;
            return scorePercentage >= PassingScore;
        }
    }
}
