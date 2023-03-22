using QuestionServiceAPI.Middlewares;

namespace QuestionServiceAPI.Extensions
{
    public static class ApiExtensions
    {
        public static void useErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
