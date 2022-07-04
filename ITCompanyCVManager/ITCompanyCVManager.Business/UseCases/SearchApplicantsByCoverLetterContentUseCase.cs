using AutoMapper;
using ITCompanyCVManager.Boundary.Context.Admin;
using ITCompanyCVManager.Boundary.Context.Common;
using ITCompanyCVManager.Domain.ElasticIndex;
using ITCompanyCVManager.Domain.Services;
using MediatR;
using Nest;

namespace ITCompanyCVManager.Business.UseCases;

public class SearchApplicantsByCoverLetterContentUseCase :
    IRequestHandler<SearchApplicantsByCoverLetterContentRequest, SearchApplicantsByCoverLetterContentResponse>
{
    private readonly IElasticClient _elasticClient;
    private readonly IMapResponseWithHighlightsService _mapResponseWithHighlightsService;
    private readonly IMapper _mapper;

    public SearchApplicantsByCoverLetterContentUseCase(IElasticClient elasticClient,
        IMapper mapper,
        IMapResponseWithHighlightsService mapResponseWithHighlightsService)
    {
        _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mapResponseWithHighlightsService = mapResponseWithHighlightsService ??
                                            throw new ArgumentNullException(nameof(mapResponseWithHighlightsService));
    }

    public async Task<SearchApplicantsByCoverLetterContentResponse> Handle(SearchApplicantsByCoverLetterContentRequest request, CancellationToken cancellationToken)
    {
        var searchResponse = await _elasticClient.SearchAsync<Application>(s => s
            .Query(queryContainer => queryContainer
                .Bool(boolQuery => boolQuery
                    .Must(must => must
                        .QueryString(queryString => queryString
                            .Fields(fields => fields.Field(application => application.CoverLetterContent))
                            .Query(request.Content)
                        )
                    )
                )
            ).Highlight(highlight => highlight
                .Fields(highlightField => highlightField
                    .Field(application => application .CoverLetterContent)
                    .PreTags("<em><b class='highlight'>")
                    .PostTags("</b></em>")
                )), cancellationToken);

        var searchResults =
            _mapper.Map<List<ResultWithHighlightsResponse>>(_mapResponseWithHighlightsService.Map(searchResponse));

        var result = new SearchApplicantsByCoverLetterContentResponse
        {
            SearchResults = searchResults
        };

        return result;
    }
}