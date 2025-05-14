using QuizApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Application.Interfaces
{
    public interface IQuizRepository
    {
        Task<Quiz> GetQuizById(int quizId);
        Task<Question> GetQuestionById(int questionId);
        Task<Answer> GetAnswerById(int answerId);
        Task<QuizAttempt> GetQuizAttemptById(int attemptId);
        Task<int> AddUserAnswer(UserAnswer userAnswer);
        Task<int> UpdateQuizAttempt(QuizAttempt quizAttempt);
    }
}
