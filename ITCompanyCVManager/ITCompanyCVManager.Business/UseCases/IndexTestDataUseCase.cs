using ITCompanyCVManager.Boundary.Context.Admin;
using ITCompanyCVManager.Persistence.Helpers;
using MediatR;
using Nest;

namespace ITCompanyCVManager.Business.UseCases;

public class IndexTestDataUseCase :
    IRequestHandler<IndexTestDataRequest>
{
    private readonly IElasticClient _elasticClient;
    public IndexTestDataUseCase(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
    }

    public async Task<Unit> Handle(IndexTestDataRequest request, CancellationToken cancellationToken)
    {
        foreach (var application in IndexTestData.GetTestData())
        {
            var response = await _elasticClient.CreateDocumentAsync(application, cancellationToken);
        }

        return Unit.Value;
    }
}