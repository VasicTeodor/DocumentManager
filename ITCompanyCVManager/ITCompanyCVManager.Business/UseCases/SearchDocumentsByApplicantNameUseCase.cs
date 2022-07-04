using AutoMapper;
using ITCompanyCVManager.Boundary.Context.Admin;
using ITCompanyCVManager.Boundary.Context.Common;
using ITCompanyCVManager.Domain.ElasticIndex;
using ITCompanyCVManager.Domain.Services;
using MediatR;
using Nest;

namespace ITCompanyCVManager.Business.UseCases;

public class SearchDocumentsByApplicantNameUseCase : 
    IRequestHandler<SearchDocumentsByApplicantNameRequest, SearchDocumentsByApplicantNameResponse>
{
    private readonly IElasticClient _elasticClient;
    private readonly IMapResponseWithHighlightsService _mapResponseWithHighlightsService;
    private readonly IMapper _mapper;

    public SearchDocumentsByApplicantNameUseCase(IElasticClient elasticClient,
        IMapper mapper,
        IMapResponseWithHighlightsService mapResponseWithHighlightsService)
    {
        _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mapResponseWithHighlightsService = mapResponseWithHighlightsService ??
                                            throw new ArgumentNullException(nameof(mapResponseWithHighlightsService));
    }
    public async Task<SearchDocumentsByApplicantNameResponse> Handle(SearchDocumentsByApplicantNameRequest request, CancellationToken cancellationToken)
    {
        var searchResponse = await _elasticClient.SearchAsync<Application>(s => s
            .Query(q => q
                .Bool(b => b
                    .Should(mu => mu
                            .Match(m => m
                                .Field(f => f.ApplicantFirstname)
                                .Query("*" + request.Firstname + "*")
                            ), mu => mu
                            .Match(m => m
                                .Field(f => f.ApplicantLastname)
                                .Query("*" + request.Lastname + "*")
                            )
                    )
                )
            ), cancellationToken);

        var searchResults =
            _mapper.Map<List<ResultWithHighlightsResponse>>(_mapResponseWithHighlightsService.Map(searchResponse));

        var result = new SearchDocumentsByApplicantNameResponse
        {
            SearchResults = searchResults
        };

        return result;
    }
}