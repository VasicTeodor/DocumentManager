using System.Net;
using AutoWrapper.Wrappers;
using FluentValidation;
using ITCompanyCVManager.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITCompanyCVManager.Api.PipelineBehavior;

public class ValidationRequestBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationRequestBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (!_validators.Any())
        {
            var response = await next();
            return response;
        }

        var validationTasks = _validators.Select(validator => validator.ValidateAsync(request, cancellationToken));
        var validationResults = await Task.WhenAll(validationTasks);

        if (validationResults.Any(result => result.IsValid))
        {
            var response = await next();
            return response;
        }

        var validationErrorsDictionary = validationResults
            .SelectMany(validationResult => validationResult.Errors)
            .GroupBy(validationFailure => validationFailure.PropertyName,
                validationFailure => validationFailure.ErrorMessage,
                (propertyName, propertyValidationErrors) => new
                {
                    Key = propertyName,
                    Errors = propertyValidationErrors.ToArray()
                })
            .ToDictionary(validationFailure => validationFailure.Key, validationFailure => validationFailure.Errors);
        var errors = new ValidationProblemDetails(validationErrorsDictionary)
        {
            Type = ErrorCode.ClientRequestDataValidationError.Code.ToString(),
            Status = (int)HttpStatusCode.BadRequest,
            Title = "Bad request",
            Detail = "One or more data validation errors occurred."
        };
        throw new ApiProblemDetailsException(errors);
    }
}