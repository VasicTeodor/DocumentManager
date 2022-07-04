using FluentValidation;
using ITCompanyCVManager.Boundary.Validators;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ITCompanyCVManager.Boundary.Context.User;

public record CreateApplicationRequest : 
    IRequest<CreateApplicationResponse>
{
    public string ApplicantFirstname { get; set; }
    public string ApplicantLastname { get; set; }
    public string ApplicantPhone { get; set; }
    public string ApplicantEmail { get; set; }
    public int ApplicantEducationLevel { get; set; }
    public string CityName { get; set; }
    public IFormFile CvFile { get; set; }
    public IFormFile CoverLetterFile { get; set; }
}

public record CreateApplicationResponse
{
    public Guid ApplicationId { get; set; }
}

public class CreateApplicationRequestDataValidator :
    AbstractValidator<CreateApplicationRequest>
{
    public CreateApplicationRequestDataValidator()
    {
        RuleFor(request => request.ApplicantFirstname)
            .NotEmpty();

        RuleFor(request => request.CityName)
            .NotEmpty();

        RuleFor(request => request.ApplicantLastname)
            .NotEmpty();

        RuleFor(request => request.ApplicantPhone)
            .NotEmpty()
            .MustBePhone();

        RuleFor(request => request.ApplicantEmail)
            .NotEmpty()
            .MustBeEmailAddress();

        RuleFor(request => request.ApplicantEducationLevel)
            .GreaterThan(-1)
            .LessThan(9)
            .WithMessage("Minimum education level is 0 (not educated) and maximum is 8 (Phd)");

        RuleFor(request => request.CvFile)
            .NotEmpty()
            .Must(file => file.FileName.ToLower().Contains("pdf"));

        RuleFor(request => request.CoverLetterFile)
            .NotEmpty()
            .Must(file => file.FileName.ToLower().Contains("pdf"));
    }
}