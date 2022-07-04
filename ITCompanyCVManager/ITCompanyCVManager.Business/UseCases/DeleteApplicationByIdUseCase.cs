using ITCompanyCVManager.Boundary.Context.Admin;
using ITCompanyCVManager.Domain.ElasticIndex;
using MediatR;
using Nest;

namespace ITCompanyCVManager.Business.UseCases;

public class DeleteApplicationByIdUseCase :
    IRequestHandler<DeleteApplicationByIdRequest>
{
    private readonly IElasticClient _elasticClient;
    public DeleteApplicationByIdUseCase(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
    }
    public async Task<Unit> Handle(DeleteApplicationByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _elasticClient.DeleteAsync<Application>(request.DocumentId, ct: cancellationToken);
        return Unit.Value;
    }
}