using EnterTask.Data.Exceptions;
using Microsoft.Data.SqlClient;

namespace EnterTask.WebAPI.ExceptionMiddleware
{
    internal class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.StatusCode = ex switch {
                    OperationCanceledException => StatusCodes.Status499ClientClosedRequest,
                    UnauthorizedAccessException => StatusCodes.Status403Forbidden,
                    NotFoundWithIdException => StatusCodes.Status404NotFound,
                    NotFoundWithParamException => StatusCodes.Status404NotFound,
                    LinkNotFoundException => StatusCodes.Status400BadRequest,
                    LoginAlreadyExistsException => StatusCodes.Status400BadRequest,
                    LoginAttemptFailedException => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.ContentType = "application/json";

                var response = new {
                    error = "An error occurred",
                    message = ex.Message,
                    exceptionType = ex.GetType().Name,
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
