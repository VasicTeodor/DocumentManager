using ITCompanyCVManager.Boundary.Context.Common;
using MediatR;

namespace ITCompanyCVManager.Boundary.Context.Admin;

public record GetAllApplicationsRequest :
    IRequest<GetAllApplicationsResponse>
{
}

public record GetAllApplicationsResponse
{
    public List<ResultWithHighlightsResponse> SearchResults { get; set; }
}