using ITCompanyCVManager.Boundary.Context.Common;
using MediatR;

namespace ITCompanyCVManager.Boundary.Context.Admin;

public record SearchApplicantsByCoverLetterContentRequest :
    IRequest<SearchApplicantsByCoverLetterContentResponse>
{
    public string Content { get; init; }
}

public record SearchApplicantsByCoverLetterContentResponse
{
    public List<ResultWithHighlightsResponse> SearchResults { get; set; }
}