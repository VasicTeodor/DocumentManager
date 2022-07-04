using AutoMapper;
using ITCompanyCVManager.Boundary.Context.Admin;
using ITCompanyCVManager.Boundary.Context.Common;
using ITCompanyCVManager.Domain.ElasticIndex;
using ITCompanyCVManager.Domain.Services;
using MediatR;
using Nest;

namespace ITCompanyCVManager.Business.UseCases;

public class SearchByCombinedParametersUseCase :
    IRequestHandler<SearchByCombinedParametersRequest, SearchByCombinedParametersResponse> 
{
    private readonly IElasticClient _elasticClient;
    private readonly IMapResponseWithHighlightsService _mapResponseWithHighlightsService;
    private readonly IMapper _mapper;

    public SearchByCombinedParametersUseCase(IElasticClient elasticClient,
        IMapper mapper,
        IMapResponseWithHighlightsService mapResponseWithHighlightsService)
    {
        _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mapResponseWithHighlightsService = mapResponseWithHighlightsService ??
                                            throw new ArgumentNullException(nameof(mapResponseWithHighlightsService));
    }
    
    public async Task<SearchByCombinedParametersResponse> Handle(SearchByCombinedParametersRequest request, CancellationToken cancellationToken)
    {
        var firstnameQuery = new MatchQuery()
        {
            Field = Infer.Field<Application>(path => path.ApplicantFirstname),
            Query = request.ApplicantFirstname
        };

        var lastNameQuery = new MatchQuery()
        {
            Field = Infer.Field<Application>(path => path.ApplicantLastname),
            Query = request.ApplicantLastname
        };

        var educationLevelQuery = new MatchQuery()
        {
            Field = Infer.Field<Application>(path => path.ApplicantEducationLevel),
            Query = request.ApplicantEducationLevel.ToString()
        };

        var coverLetterContentQuery = new MatchQuery()
        {
            Field = Infer.Field<Application>(path => path.CoverLetterContent),
            Query = request.CoverLetterContent
        };

        var must = new List<QueryContainer>();
        var should = new List<QueryContainer>();

        if (request.FirstOperator == QueryOperator.AND)
        {
            must.Add(firstnameQuery);
            must.Add(lastNameQuery);
        }
        else
        {
            should.Add(firstnameQuery);
            should.Add(lastNameQuery);
        }

        if (request.SecondOperator == QueryOperator.AND)
        {
            if (!must.Contains(lastNameQuery))
            {
                must.Add(lastNameQuery);
                should.Remove(lastNameQuery);
            }

            must.Add(educationLevelQuery);
        }
        else
        {
            should.Add(educationLevelQuery);
        }


        if (request.ThirdOperator == QueryOperator.AND)
        {
            if (!must.Contains(educationLevelQuery))
            {
                must.Add(educationLevelQuery);
                should.Remove(educationLevelQuery);
            }

            must.Add(coverLetterContentQuery);
        }
        else
        {
            should.Add(coverLetterContentQuery);
        }

        var searchResponse = await _elasticClient.SearchAsync<Application>(new SearchRequest<Application>
        {
            Query = new BoolQuery()
            {
                Must = must,
                Should = should
            },
            Highlight = new Highlight
            {
                PreTags = new[] { "<em><b class='highlight'>" },
                PostTags = new[] { "</b><em>" },
                Encoder = HighlighterEncoder.Html,
                Fields = new Dictionary<Field, IHighlightField>
                {
                    {
                        "cvContent", new HighlightField
                        {
                            Type = HighlighterType.Plain,
                            ForceSource = true,
                        }
                    }
                }
            }
        }, cancellationToken);

        var searchResults =
            _mapper.Map<List<ResultWithHighlightsResponse>>(_mapResponseWithHighlightsService.Map(searchResponse));

        var result = new SearchByCombinedParametersResponse
        {
            SearchResults = searchResults
        };

        return result;
    }
}