using FluentValidation;
using ITCompanyCVManager.Boundary.Context.Common;
using MediatR;

namespace ITCompanyCVManager.Boundary.Context.Admin;

public record GetApplicantsByGeoLocationRequest :
    IRequest<GetApplicantsByGeoLocationResponse>
{
    public string City { get; set; }
    public int Radius { get; set; }
}

public record GetApplicantsByGeoLocationResponse
{
    public List<ResultWithHighlightsResponse> SearchResults { get; set; }
}

public class GetApplicantsByGeoLocationRequestDataValidator :
    AbstractValidator<GetApplicantsByGeoLocationRequest>
{
    public GetApplicantsByGeoLocationRequestDataValidator()
    {
        RuleFor(request => request.City)
            .NotEmpty();

        RuleFor(request => request.Radius)
            .GreaterThan(0);
    }
}