using Application;
using Persistence;
using QuestionServiceAPI.Extensions;
using Shared;

namespace QuestionServiceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddPersitence(configuration);
            builder.Services.AddAplicationLayer(configuration);
            builder.Services.AddSharedInfraestructure();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.useErrorHandlingMiddleware();

            app.MapControllers();

            app.Run();
        }
    }
}