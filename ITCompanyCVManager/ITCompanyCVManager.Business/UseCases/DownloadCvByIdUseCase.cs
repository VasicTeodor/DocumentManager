using ITCompanyCVManager.Boundary.Context.Admin;
using ITCompanyCVManager.Domain.ElasticIndex;
using MediatR;
using Nest;

namespace ITCompanyCVManager.Business.UseCases;

public class DownloadCvByIdUseCase :
    IRequestHandler<DownloadCvByIdRequest, DownloadCvByIdResponse>
{
    private readonly IElasticClient _elasticClient;
    public DownloadCvByIdUseCase(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
    }
    public async Task<DownloadCvByIdResponse> Handle(DownloadCvByIdRequest request, CancellationToken cancellationToken)
    {
        var response = await _elasticClient.GetAsync<Application>(request.DocumentId, 
            getDescriptor => getDescriptor.Index("cv_management"), cancellationToken);
        var document = response.Source;
        var cvFilePath =
            @$"JobApplications/{document.DateCreated.ToString("d")}/{document.Id}/cv-{document.CvFileName}";

        FileStream stream = new FileStream(cvFilePath, FileMode.Open);
        return new DownloadCvByIdResponse
        {
            CvContent = stream,
            CvName = document.CvFileName
        };
    }
}