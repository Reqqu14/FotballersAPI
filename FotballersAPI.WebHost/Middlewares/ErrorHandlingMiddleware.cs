using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FotballersAPI.WebHost.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }

            catch (ValidationException e)
            {
                var problemDetails = GetBadRequestValidationProblemDetails(e);

                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
            }
        }

        private ValidationProblemDetails GetBadRequestValidationProblemDetails(ValidationException ex)
        {
            string traceId = Guid.NewGuid().ToString();

            var errors = new Dictionary<string, string[]>();
            foreach (var error in ex.Errors)
            {
                errors.Add(error.PropertyName, new string[] { error.ErrorMessage });
            }

            var validationProblemDetails = new ValidationProblemDetails(errors);

            validationProblemDetails.Status = 400;
            validationProblemDetails.Type = "https://httpstatuses.com/400";
            validationProblemDetails.Title = "Validation failed";
            validationProblemDetails.Detail = "One or more inputs need to be corrected. Check errors for details";
            validationProblemDetails.Instance = traceId;

            return validationProblemDetails;
        }
    }
}
