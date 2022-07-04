using ITCompanyCVManager.Boundary.Context.Common;
using MediatR;

namespace ITCompanyCVManager.Boundary.Context.Admin;

public record SearchApplicantsByPhraseRequest : IRequest<SearchApplicantsByPhraseResponse>
{
    public string PhraseQuery { get; init; }
}

public record SearchApplicantsByPhraseResponse
{
    public List<ResultWithHighlightsResponse> SearchResults { get; set; }
}