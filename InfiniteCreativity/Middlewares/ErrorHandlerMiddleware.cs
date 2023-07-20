using InfiniteCreativity.Exceptions;
using System.Net;
using System.Text.Json;

namespace InfiniteCreativity.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
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
                var body = new Dictionary<string,string> { { "message" , error?.Message } };

                switch (error)
                {
                    case ExceptionBase e:
                        body.Add("code", e.Code);
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(body);
                await response.WriteAsync(result);
            }
        }
    }
}
