using QuizApp.Application.Dtos;
using QuizApp.Application.Interfaces;
using QuizApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
    public class QuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<IEnumerable<QuestionDto>> GetQuestionsAsync(int quizId)
        {
            var quiz = await _quizRepository.GetQuizById(quizId);
            if (quiz == null)
            {
                throw new KeyNotFoundException("Quiz not found.");
            }

            return quiz.Questions.Select(q => new QuestionDto
            {
                QuestionId = q.QuestionId,
                QuestionText = q.QuestionText,
                Answers = q.Answers.Select(a => new AnswerDto
                {
                    AnswerId = a.AnswerId,
                    AnswerText = a.AnswerText
                }).ToList()
            });
        }

        public async Task<AnswerFeedbackDto> ValidateAnswerAsync(UserAnswerDto userAnswerDto)
        {
            var answer = await _quizRepository.GetAnswerById(userAnswerDto.SelectedAnswerId);
            if (answer == null || answer.QuestionId != userAnswerDto.QuestionId)
            {
                throw new KeyNotFoundException("Invalid question or answer ID.");
            }

            var quizAttempt = await _quizRepository.GetQuizAttemptById(userAnswerDto.AttemptId);
            if (quizAttempt == null)
            {
                throw new KeyNotFoundException("Quiz attempt not found.");
            }

            var userAnswer = new UserAnswer
            {
                AttemptId = userAnswerDto.AttemptId,
                QuestionId = userAnswerDto.QuestionId,
                SelectedAnswerId = userAnswerDto.QuestionId,
                IsCorrect = answer.IsCorrect
            };

            await _quizRepository.AddUserAnswer(userAnswer);

            var correctAnswer = quizAttempt.Quiz.Questions
                .First(q => q.QuestionId == userAnswerDto.QuestionId)
                .Answers.FirstOrDefault(a => a.IsCorrect);

            var feedback = new AnswerFeedbackDto
            {
                QuestionId = userAnswerDto.QuestionId,
                SelectedAnswerId = userAnswerDto.SelectedAnswerId,
                IsCorrect = answer.IsCorrect,
                Feedback = answer.IsCorrect ? "Correct!" : $"Incorrect. The correct answer is: {correctAnswer?.AnswerText}"
            };

            return feedback; 
        }

        public async Task<QuizResultDto> GetQuizResultsAsync(int attemptId)
        {
            var quizAttempt = await _quizRepository.GetQuizAttemptById(attemptId);
            if (quizAttempt == null)
            {
                throw new KeyNotFoundException("Quiz attempt not found or not completed.");
            }

            var userAnswers = quizAttempt.UserAnswers;
            var correctAnswers = userAnswers.Count(ua => ua.IsCorrect);
            var totalQuestions = quizAttempt.Quiz.TotalQuestions;
            var incorrectAnswers = totalQuestions - correctAnswers;
            var passed = quizAttempt.Quiz.EvaluateQuizResult(correctAnswers);

            quizAttempt.Score = correctAnswers;
            quizAttempt.Passed = passed;
            await _quizRepository.UpdateQuizAttempt(quizAttempt);

            var totalTime = quizAttempt.EndTime.Value - quizAttempt.StartTime;

            return new QuizResultDto
            {
                AttemptId = attemptId,
                TotalTime = $"{totalTime.Minutes}m {totalTime.Seconds}s",
                CorrectAnswers = correctAnswers,
                IncorrectAnswers = incorrectAnswers,
                PassFail = passed ? "Passed" : "Failed"
            };
        }
    }
}