using ITCompanyCVManager.Boundary.Context.Common;
using MediatR;

namespace ITCompanyCVManager.Boundary.Context.Admin;

public record SearchByCombinedParametersRequest :
    IRequest<SearchByCombinedParametersResponse>
{
    public string ApplicantFirstname { get; init; }
    public string ApplicantLastname { get; init; }
    public int ApplicantEducationLevel { get; init; }
    public string CoverLetterContent { get; init; }
    public QueryOperator FirstOperator { get; init; }
    public QueryOperator SecondOperator { get; init; }
    public QueryOperator ThirdOperator { get; init; }
}

public record SearchByCombinedParametersResponse
{
    public List<ResultWithHighlightsResponse> SearchResults { get; set; }
}

public enum QueryOperator
{
    AND,
    OR
}