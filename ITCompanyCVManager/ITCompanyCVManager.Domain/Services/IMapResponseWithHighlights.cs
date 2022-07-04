using ITCompanyCVManager.Domain.ElasticIndex;
using ITCompanyCVManager.Domain.Services.Models;
using Nest;

namespace ITCompanyCVManager.Domain.Services;

public interface IMapResponseWithHighlightsService
{
    List<ResultWithHighlights> Map(ISearchResponse<Application> searchResponse);
}