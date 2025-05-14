using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Domain.EF
{
    public class QuizAppDbContext : DbContext
    {
        public QuizAppDbContext(DbContextOptions<QuizAppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Using Fluent API to configure the model

            // User
            modelBuilder.Entity<User>()
               .HasKey(u => u.UserId);
            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(256);

            // Quiz
            modelBuilder.Entity<Quiz>()
                .HasKey(q => q.QuizId);
            modelBuilder.Entity<Quiz>()
                .Property(q => q.Title)
                .IsRequired()
                .HasMaxLength(200);
            modelBuilder.Entity<Quiz>()
                .Property(q => q.TotalQuestions)
                .IsRequired();
            modelBuilder.Entity<Quiz>()
                .Property(q => q.PassingScore)
                .IsRequired();

            // Question
            modelBuilder.Entity<Question>()
                .HasKey(q => q.QuestionId);
            modelBuilder.Entity<Question>()
                .Property(q => q.QuestionText)
                .IsRequired()
                .HasMaxLength(500);
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Quiz)
                .WithMany(qz => qz.Questions)
                .HasForeignKey(q => q.QuizId);

            // Answer
            modelBuilder.Entity<Answer>()
                .HasKey(a => a.AnswerId);
            modelBuilder.Entity<Answer>()
                .Property(a => a.AnswerText)
                .IsRequired()
                .HasMaxLength(200);
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId);

            // QuizAttempt
            modelBuilder.Entity<QuizAttempt>()
                .HasKey(qa => qa.AttemptId);
            modelBuilder.Entity<QuizAttempt>()
                .HasOne(qa => qa.User)
                .WithMany(u => u.QuizAttempts)
                .HasForeignKey(qa => qa.UserId);
            modelBuilder.Entity<QuizAttempt>()
                .HasOne(qa => qa.Quiz)
                .WithMany(qz => qz.QuizAttempts)
                .HasForeignKey(qa => qa.QuizId);

            modelBuilder.Entity<UserAnswer>()
    .HasKey(ua => ua.UserAnswerId);

            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.Question)
                .WithMany()
                .HasForeignKey(ua => ua.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.Answer)
                .WithMany()
                .HasForeignKey(ua => ua.SelectedAnswerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.QuizAttempt)
                .WithMany()
                .HasForeignKey(ua => ua.AttemptId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Seed();
        }
    }
}