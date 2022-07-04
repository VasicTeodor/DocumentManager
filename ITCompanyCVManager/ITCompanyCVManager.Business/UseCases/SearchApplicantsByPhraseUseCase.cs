using AutoMapper;
using ITCompanyCVManager.Boundary.Context.Admin;
using ITCompanyCVManager.Boundary.Context.Common;
using ITCompanyCVManager.Domain.ElasticIndex;
using ITCompanyCVManager.Domain.Services;
using MediatR;
using Nest;

namespace ITCompanyCVManager.Business.UseCases;

public class SearchApplicantsByPhraseUseCase :
    IRequestHandler<SearchApplicantsByPhraseRequest, SearchApplicantsByPhraseResponse>
{
    private readonly IElasticClient _elasticClient;
    private readonly IMapResponseWithHighlightsService _mapResponseWithHighlightsService;
    private readonly IMapper _mapper;

    public SearchApplicantsByPhraseUseCase(IElasticClient elasticClient,
        IMapper mapper,
        IMapResponseWithHighlightsService mapResponseWithHighlightsService)
    {
        _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mapResponseWithHighlightsService = mapResponseWithHighlightsService ??
                                            throw new ArgumentNullException(nameof(mapResponseWithHighlightsService));
    }

    public async Task<SearchApplicantsByPhraseResponse> Handle(SearchApplicantsByPhraseRequest request, CancellationToken cancellationToken)
    {
        var searchResponse = await _elasticClient.SearchAsync<Application>(s => s
            .Query(queryContainer => queryContainer
                .MultiMatch(multiMatch => multiMatch
                    .Fields(fields => fields
                        .Field(application => application.ApplicantFirstname)
                        .Field(application => application.ApplicantLastname)
                        .Field(application => application.ApplicantPhone)
                        .Field(application => application.ApplicantEmail)
                        .Field(application => application.CityName)
                        .Field(application => application.CvContent)
                        .Field(application => application.CoverLetterContent)
                    )
                    .Query(request.PhraseQuery)
                    .Type(TextQueryType.Phrase)
                )
            ).Highlight(highLights => highLights
                .Fields(field => field
                    .Field("*")
                    .PreTags("<em><b class='highlight'>")
                    .PostTags("</b></em>")
                )), cancellationToken);

        var searchResults =
            _mapper.Map<List<ResultWithHighlightsResponse>>(_mapResponseWithHighlightsService.Map(searchResponse));

        var result = new SearchApplicantsByPhraseResponse
        {
            SearchResults = searchResults
        };

        return result;
    }
}