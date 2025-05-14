using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Interfaces;
using QuizApp.Domain.EF;
using QuizApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure
{
    public class QuizRepository : IQuizRepository
    {
        private readonly QuizAppDbContext _context;

        public QuizRepository(QuizAppDbContext context)
        {
            _context = context;
        }
        public async Task<Quiz> GetQuizById(int quizId)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.QuizId == quizId);
            return quiz;
        }
        public async Task<Question> GetQuestionById(int questionId)
        {
            var question =  await _context.Questions
                .FirstOrDefaultAsync(q => q.QuestionId == questionId);
            return question;
        }
        public async Task<Answer> GetAnswerById(int answerId)
        {
            var anwser = await _context.Answers
                .FirstOrDefaultAsync(a => a.AnswerId == answerId);
            return anwser;
        }
        public async Task<QuizAttempt> GetQuizAttemptById(int attemptId)
        {
            return await _context.QuizAttempts
                .Include(qa => qa.UserAnswers)
                .Include(qa => qa.Quiz)
                .FirstOrDefaultAsync(qa => qa.AttemptId == attemptId);
        }
        public async Task<int> AddUserAnswer(UserAnswer userAnswer)
        {
            _context.UserAnswers.AddAsync(userAnswer);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateQuizAttempt(QuizAttempt quizAttempt)
        {
            _context.QuizAttempts.Update(quizAttempt);
            return await _context.SaveChangesAsync();
        }
    }
}
