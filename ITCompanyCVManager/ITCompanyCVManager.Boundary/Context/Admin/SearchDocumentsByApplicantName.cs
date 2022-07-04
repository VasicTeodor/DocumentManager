using FluentValidation;
using ITCompanyCVManager.Boundary.Context.Common;
using MediatR;

namespace ITCompanyCVManager.Boundary.Context.Admin;

public record SearchDocumentsByApplicantNameRequest : 
    IRequest<SearchDocumentsByApplicantNameResponse>
{
    public string Firstname { get; init; }
    public string Lastname { get; init; }
}

public record SearchDocumentsByApplicantNameResponse
{
    public List<ResultWithHighlightsResponse> SearchResults { get; set; }
}

public class SearchDocumentsByApplicantNameRequestDataValidator :
    AbstractValidator<SearchDocumentsByApplicantNameRequest>
{
    public SearchDocumentsByApplicantNameRequestDataValidator()
    {
        RuleFor(request => request.Firstname)
            .NotEmpty();

        RuleFor(request => request.Lastname)
            .NotEmpty();
    }
}