using ITCompanyCVManager.Business.Contracts.Persistence;
using MediatR;

namespace ITCompanyCVManager.Api.PipelineBehavior;

public class TransactionalBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : notnull
{
    private readonly IApplicationUnitOfWork _unitOfWork;
    private readonly ILogger<TransactionalBehavior<TRequest, TResponse>> _logger;
    private readonly IRequestHandler<TRequest, TResponse> _useCase;

    public TransactionalBehavior(IApplicationUnitOfWork unitOfWork,
                                 ILogger<TransactionalBehavior<TRequest, TResponse>> logger,
                                 IRequestHandler<TRequest, TResponse> useCase)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            var response = await next();
            await _unitOfWork.Complete(cancellationToken);
            return response;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Pipeline error");
            _unitOfWork.Rollback();
            throw;
        }
    }
}