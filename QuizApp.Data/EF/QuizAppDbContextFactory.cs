using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Domain.EF
{
    public class EShopDbContextFactory : IDesignTimeDbContextFactory<QuizAppDbContext>
    {
        public QuizAppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("QuizAppDB");
            var optionsBuilder = new DbContextOptionsBuilder<QuizAppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new QuizAppDbContext(optionsBuilder.Options);
        }
    }
}
