using System.Net;
using ITCompanyCVManager.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;

namespace ITCompanyCVManager.Api.Configuration;

public class ApiBehaviorOptionsConfigurator :
    IConfigureOptions<ApiBehaviorOptions>
{
    public void Configure(ApiBehaviorOptions options)
    {
        options.InvalidModelStateResponseFactory = ProblemDetailsInvalidModelStateResponseFactory;
    }

    private static IActionResult ProblemDetailsInvalidModelStateResponseFactory(ActionContext context)
    {
        var problemDetails = new ValidationProblemDetails(context.ModelState)
        {
            Type = ErrorCode.ClientRequestDataValidationError.ToString(),
            Status = (int)HttpStatusCode.BadRequest,
            Title = "Bad request",
            Detail = "One or more validation errors occurred.",
            Instance = context.HttpContext.Request.Path
        };

        return new BadRequestObjectResult(problemDetails)
        {
            ContentTypes = new MediaTypeCollection { "application/json" }
        };
    }
}