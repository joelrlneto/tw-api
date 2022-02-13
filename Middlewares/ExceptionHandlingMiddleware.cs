using FluentValidation;
using System.Net;
using System.Text.Json;

namespace WebApplication3.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
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

                switch (error)
                {
                    case ValidationException e:
                        response.ContentType = "application/json";
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        var result = JsonSerializer.Serialize(new { erros = e.Errors.Select(erro => erro.ErrorMessage) });
                        await response.WriteAsync(result);
                        break;
                }
            }
        }
    }
}
