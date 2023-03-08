using BitrateCalculation.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace BitrateCalculation.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (ex is ApiException)
                    throw;
                else
                    await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            var problemDetails = new ProblemDetails()
            {
                Type = "https://demo.api.com/errors/internal-server-error",
                Title = "Unrecoverable error occured",
                Status = StatusCodes.Status500InternalServerError,
                Detail = exception.Message
            };
            problemDetails.Extensions.Add("RequestId", context.TraceIdentifier);

            var exceptionResponse = new ApiExceptionResponse()
            {
                Message = "Error has occured",
                ExceptionMessage = exception.Message,
                ExceptionType = exception.GetType().ToString(),
                StackTrace = exception.StackTrace
            };

            var result = JsonConvert.SerializeObject(exceptionResponse);
            _logger.LogError(result);
            _logger.LogError("Internal Exception: {Exception}", exception.InnerException);
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(result);
        }
    }
}
