using System.Net;
using AutoWrapper.Wrappers;
using ITCompanyCVManager.Domain.Exceptions.Status;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITCompanyCVManager.Api.PipelineBehavior;

public class ExceptionHandlingBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> _logger;
    private readonly IRequestHandler<TRequest, TResponse> _useCase;

    public ExceptionHandlingBehavior(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger,
        IRequestHandler<TRequest, TResponse> useCase)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            var response = await next();
            return response;
        }
        catch (BadRequestException badRequestException)
        {
            _logger.LogError(badRequestException, badRequestException.Message);
            throw new ApiProblemDetailsException(new ProblemDetails
            {
                Title = badRequestException.Title,
                Type = badRequestException.Code.ToString(),
                Status = (int)HttpStatusCode.BadRequest,
                Detail = badRequestException.Message
            });
        }
        catch (ForbiddenException forbiddenException)
        {
            _logger.LogError(forbiddenException, forbiddenException.Message);
            throw new ApiProblemDetailsException(new ProblemDetails
            {
                Title = forbiddenException.Title,
                Type = forbiddenException.Code.ToString(),
                Status = (int)HttpStatusCode.Forbidden,
                Detail = forbiddenException.Message
            });
        }
        catch (NotFoundException notFoundEntityException)
        {
            _logger.LogError(notFoundEntityException, notFoundEntityException.Message);
            throw new ApiProblemDetailsException(new ProblemDetails
            {
                Title = notFoundEntityException.Title,
                Type = notFoundEntityException.Code.ToString(),
                Status = (int)HttpStatusCode.NotFound,
                Detail = notFoundEntityException.Message
            });
        }
        catch (UnsupportedMediaTypeException unsupportedMediaTypeException)
        {
            _logger.LogError(unsupportedMediaTypeException, unsupportedMediaTypeException.Message);
            throw new ApiProblemDetailsException(new ProblemDetails
            {
                Title = unsupportedMediaTypeException.Title,
                Type = unsupportedMediaTypeException.Code.ToString(),
                Status = (int)HttpStatusCode.UnsupportedMediaType,
                Detail = unsupportedMediaTypeException.Message
            });
        }
        catch (UnprocessableEntityException unprocessableEntityException)
        {
            _logger.LogError(unprocessableEntityException, unprocessableEntityException.Message);
            throw new ApiProblemDetailsException(new ProblemDetails
            {
                Title = unprocessableEntityException.Title,
                Type = unprocessableEntityException.Code.ToString(),
                Status = (int)HttpStatusCode.UnprocessableEntity,
                Detail = unprocessableEntityException.Message
            });
        }
        catch (InternalServerErrorException internalServerErrorException)
        {
            _logger.LogError(internalServerErrorException, internalServerErrorException.Message);
            throw new ApiProblemDetailsException(new ProblemDetails
            {
                Title = internalServerErrorException.Title,
                Type = internalServerErrorException.Code.ToString(),
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = internalServerErrorException.Message
            });
        }
        catch (ServiceUnavailableException serviceUnavailableException)
        {
            _logger.LogError(serviceUnavailableException, serviceUnavailableException.Message);
            throw new ApiProblemDetailsException(new ProblemDetails
            {
                Title = serviceUnavailableException.Title,
                Type = serviceUnavailableException.Code.ToString(),
                Status = (int)HttpStatusCode.ServiceUnavailable,
                Detail = serviceUnavailableException.Message
            });
        }
        catch (ServiceResponseErrorException serviceResponseErrorException)
        {
            _logger.LogError(serviceResponseErrorException, serviceResponseErrorException.Message);
            throw new ApiProblemDetailsException(new ProblemDetails
            {
                Title = serviceResponseErrorException.Title,
                Type = serviceResponseErrorException.Code.ToString(),
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = serviceResponseErrorException.Message
            });
        }
        catch (Domain.Exceptions.ApplicationException applicationException)
        {
            _logger.LogError(applicationException, applicationException.Message);
            throw new ApiProblemDetailsException(new ProblemDetails
            {
                Title = "Something went wrong on application.",
                Type = applicationException.Code.ToString(),
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = applicationException.Code.Description
            });
        }
    }
}