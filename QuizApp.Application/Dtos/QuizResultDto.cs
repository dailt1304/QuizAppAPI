using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Application.Dtos
{
    public class QuizResultDto
    {
        public int AttemptId { get; set; }
        public string TotalTime { get; set; }
        public int CorrectAnswers { get; set; }
        public int IncorrectAnswers { get; set; }
        public string PassFail { get; set; }
    }
}
