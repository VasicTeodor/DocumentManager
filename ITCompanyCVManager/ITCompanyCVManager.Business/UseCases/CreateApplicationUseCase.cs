using AutoMapper;
using ITCompanyCVManager.Boundary.Context.User;
using ITCompanyCVManager.Business.Exceptions;
using ITCompanyCVManager.Domain.ElasticIndex;
using ITCompanyCVManager.Domain.Services;
using MediatR;
using Nest;

namespace ITCompanyCVManager.Business.UseCases;

public class CreateApplicationUseCase :
    IRequestHandler<CreateApplicationRequest, CreateApplicationResponse>
{
    private readonly IElasticClient _elasticClient;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    private readonly IGeoLocationDecodeService _geoLocationDecodeService;
    public CreateApplicationUseCase(IGeoLocationDecodeService geoLocationDecodeService,
        IElasticClient elasticClient,
        IMapper mapper,
        IFileService fileService)
    {
        _geoLocationDecodeService = geoLocationDecodeService ?? throw new ArgumentNullException(nameof(geoLocationDecodeService));
        _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
    }

    public async Task<CreateApplicationResponse> Handle(CreateApplicationRequest request, CancellationToken cancellationToken)
    {
        var city = await _geoLocationDecodeService.DecodeCityLatLong(request.CityName);

        var newApplication = _mapper.Map<Application>(request);
        newApplication.LatLongLocation = new GeoLocation(city.Latitude, city.Longitude);
        newApplication.DateCreated = DateTime.UtcNow;
        newApplication.Id = Guid.NewGuid();
        newApplication.CvContent = _fileService.ReadTextFromPdfFile(request.CvFile);
        newApplication.CoverLetterContent = _fileService.ReadTextFromPdfFile(request.CoverLetterFile);
        newApplication.CvFileName = request.CvFile.FileName;
        newApplication.CoverLetterFileName = request.CoverLetterFile.FileName;

        var response = await _elasticClient.CreateDocumentAsync(newApplication, cancellationToken);

        if (response.IsValid)
        {
            var cvFilePath = @$"JobApplications/{DateTime.UtcNow.Date.ToString("d")}/{newApplication.Id}/cv-{request.CvFile.FileName}";
            var coverLetterPath = @$"JobApplications/{DateTime.UtcNow.Date.ToString("d")}/{newApplication.Id}/cover-letter-{request.CoverLetterFile.FileName}";

            _fileService.SaveFileToDirectory(cvFilePath, request.CvFile);
            _fileService.SaveFileToDirectory(coverLetterPath, request.CoverLetterFile);

            return new CreateApplicationResponse
            {
                ApplicationId = newApplication.Id
            };
        }

        throw new ElasticsearchServiceUnavailableException();
    }
}