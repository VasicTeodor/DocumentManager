using FluentValidation;
using ITCompanyCVManager.Boundary.Context.Common;
using MediatR;

namespace ITCompanyCVManager.Boundary.Context.Admin;

public record SearchApplicationsByApplicantEducationLevelRequest :
    IRequest<SearchApplicationsByApplicantEducationLevelResponse>
{
    public int? EducationLevel { get; init; }
}

public record SearchApplicationsByApplicantEducationLevelResponse
{
    public List<ResultWithHighlightsResponse> SearchResults { get; set; }
}

public class SearchApplicationsByApplicantEducationLevelRequestDataValidator :
    AbstractValidator<SearchApplicationsByApplicantEducationLevelRequest>
{
    public SearchApplicationsByApplicantEducationLevelRequestDataValidator()
    {
        RuleFor(request => request.EducationLevel)
            .GreaterThan(-1)
            .When(request => request.EducationLevel is not null)
            .LessThan(9)
            .When(request => request.EducationLevel is not null)
            .WithMessage("Minimum education level is 0 (not educated) and maximum is 8 (Phd)");
    }
}