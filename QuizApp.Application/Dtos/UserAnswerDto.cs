using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Application.Dtos
{
    public class UserAnswerDto
    {
        public int AttemptId { get; set; }
        public int QuestionId { get; set; }
        public int SelectedAnswerId { get; set; }
    }
}
