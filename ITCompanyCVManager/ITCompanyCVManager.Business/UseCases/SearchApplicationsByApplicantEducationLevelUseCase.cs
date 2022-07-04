using AutoMapper;
using ITCompanyCVManager.Boundary.Context.Admin;
using ITCompanyCVManager.Boundary.Context.Common;
using ITCompanyCVManager.Domain.ElasticIndex;
using ITCompanyCVManager.Domain.Services;
using MediatR;
using Nest;

namespace ITCompanyCVManager.Business.UseCases;

public class SearchApplicationsByApplicantEducationLevelUseCase :
    IRequestHandler<SearchApplicationsByApplicantEducationLevelRequest,
        SearchApplicationsByApplicantEducationLevelResponse>
{
    private readonly IElasticClient _elasticClient;
    private readonly IMapResponseWithHighlightsService _mapResponseWithHighlightsService;
    private readonly IMapper _mapper;

    public SearchApplicationsByApplicantEducationLevelUseCase(IElasticClient elasticClient,
        IMapper mapper,
        IMapResponseWithHighlightsService mapResponseWithHighlightsService)
    {
        _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mapResponseWithHighlightsService = mapResponseWithHighlightsService ??
                                            throw new ArgumentNullException(nameof(mapResponseWithHighlightsService));
    }

    public async Task<SearchApplicationsByApplicantEducationLevelResponse> Handle(
        SearchApplicationsByApplicantEducationLevelRequest request, CancellationToken cancellationToken)
    {
        var searchResponse = await _elasticClient.SearchAsync<Application>(
            searchDescriptor => searchDescriptor.Query(queryContainer =>
                queryContainer.Term(application => application.ApplicantEducationLevel, request.EducationLevel)), cancellationToken);

        var searchResults =
            _mapper.Map<List<ResultWithHighlightsResponse>>(_mapResponseWithHighlightsService.Map(searchResponse));

        var result = new SearchApplicationsByApplicantEducationLevelResponse
        {
            SearchResults = searchResults
        };

        return result;
    }
}