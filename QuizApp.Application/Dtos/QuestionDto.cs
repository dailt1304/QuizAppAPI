using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Application.Dtos
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
