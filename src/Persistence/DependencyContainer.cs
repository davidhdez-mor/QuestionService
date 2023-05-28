using Application.Interfaces;
using Domain.Respository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;

namespace Persistence
{
    public static class DependencyContainer 
    {
        public static IServiceCollection AddPersitence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(GetDbConnectionString());
            services.AddDbContext<QuestionContext>(options => options.UseNpgsql(connectionString));
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(MyRepositoryAsync<>));
            return services;
        }

        private static string GetDbConnectionString()
        {
            var host = Environment.GetEnvironmentVariable("DBHOST");
            var port = Environment.GetEnvironmentVariable("DBPORT");
            var user = Environment.GetEnvironmentVariable("DBUSER");
            var pass = Environment.GetEnvironmentVariable("DBPASSWORD");
            var dbName = Environment.GetEnvironmentVariable("DBNAME");
            return $"Host={host};Port={port};Database={dbName};Username={user};Password={pass}";
        }
    }
}
