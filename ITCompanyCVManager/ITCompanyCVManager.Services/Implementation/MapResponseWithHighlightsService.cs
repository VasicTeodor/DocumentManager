using AutoMapper;
using ITCompanyCVManager.Domain.ElasticIndex;
using ITCompanyCVManager.Domain.Services;
using ITCompanyCVManager.Domain.Services.Models;
using Nest;

namespace ITCompanyCVManager.Services.Implementation;

public class MapResponseWithHighlightsService :
    IMapResponseWithHighlightsService
{
    private readonly IMapper _mapper;
    public MapResponseWithHighlightsService(IMapper mapper)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public List<ResultWithHighlights> Map(ISearchResponse<Application> searchResponse)
    {
        var allResults = new List<ResultWithHighlights>();
        foreach (var document in searchResponse.Documents)
        {
            var result = new ResultWithHighlights
            {
                Application = _mapper.Map<ApplicationSearchResponse>(document),
                Highlights = new List<string>()
            };

            var documentHits = searchResponse.Hits.Where(hit => hit.Id == document.Id.ToString()).ToList();
            var highlights = documentHits.Select(hit => hit.Highlight);

            foreach (var highlight in highlights)
            {
                foreach (var highlightValue in highlight.Values)
                {
                    foreach (var value in highlightValue)
                    {
                        if (value is not null)
                        {
                            result.Highlights.Add(value);
                        }
                    }
                }
            }
            allResults.Add(result);
        }

        return allResults;
    }
}