using Application.Wrappers;
using System.Net;
using System.Text.Json;

namespace QuestionServiceAPI.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = error.Message };

                switch (error)
                {
                    case Application.Exceptios.ApiExceptions e:
                        //Custom Aplication Error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        Console.WriteLine(error.Message);
                        break;

                    case Application.Exceptions.ValidationExceptions e:
                        // Custom Application
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        Console.WriteLine(error.Message);
                        responseModel.Errors = e.Errors;
                        break;
                    case KeyNotFoundException e:
                        //Not Found Error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        Console.WriteLine(error.Message);
                        break;
                    default:
                        //unhandle Error 
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        Console.WriteLine(error.Message);
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);

            }
        }
    }
}
