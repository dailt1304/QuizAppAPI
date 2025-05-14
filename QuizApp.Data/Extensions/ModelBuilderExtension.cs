using Microsoft.EntityFrameworkCore;
using QuizApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Domain.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                UserName = "john_doe",
                Password = "password123",
                Email = "john@example.com"
            });

            modelBuilder.Entity<Quiz>().HasData(new Quiz
            {
                QuizId = 1,
                Title = "Basic Math Quiz",
                TotalQuestions = 2,
                PassingScore = 50
            });

            modelBuilder.Entity<Question>().HasData(
                new Question
                {
                    QuestionId = 1,
                    QuizId = 1,
                    QuestionText = "What is 2 + 2?"
                },
                new Question
                {
                    QuestionId = 2,
                    QuizId = 1,
                    QuestionText = "What is 3 * 3?"
                }
            );

            modelBuilder.Entity<Answer>().HasData(
                new Answer
                {
                    AnswerId = 1,
                    QuestionId = 1,
                    AnswerText = "4",
                    IsCorrect = true
                },
                new Answer
                {
                    AnswerId = 2,
                    QuestionId = 1,
                    AnswerText = "5",
                    IsCorrect = false
                },
                new Answer
                {
                    AnswerId = 3,
                    QuestionId = 2,
                    AnswerText = "9",
                    IsCorrect = true
                },
                new Answer
                {
                    AnswerId = 4,
                    QuestionId = 2,
                    AnswerText = "6",
                    IsCorrect = false
                }
            );

            modelBuilder.Entity<QuizAttempt>().HasData(new QuizAttempt
            {
                AttemptId = 1,
                UserId = 1,
                QuizId = 1,
                Score = 100,
                Passed = true,
                StartTime = new DateTime(2024, 01, 01, 10, 00, 00, DateTimeKind.Utc),
                EndTime = new DateTime(2024, 01, 01, 10, 10, 00, DateTimeKind.Utc)
            });


            modelBuilder.Entity<UserAnswer>().HasData(
                new UserAnswer
                {
                    UserAnswerId = 1,
                    AttemptId = 1,
                    QuestionId = 1,
                    SelectedAnswerId = 1,
                    IsCorrect = true
                },
                new UserAnswer
                {
                    UserAnswerId = 2,
                    AttemptId = 1,
                    QuestionId = 2,
                    SelectedAnswerId = 3,
                    IsCorrect = true
                }
            );
        }

    }
}
