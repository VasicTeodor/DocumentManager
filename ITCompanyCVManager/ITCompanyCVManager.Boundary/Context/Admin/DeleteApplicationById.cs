using FluentValidation;
using MediatR;

namespace ITCompanyCVManager.Boundary.Context.Admin;

public record DeleteApplicationByIdRequest :
    IRequest
{
    public Guid DocumentId { get; init; }
}

public class DeleteApplicationByIdRequestDataValidator :
    AbstractValidator<DeleteApplicationByIdRequest>
{
    public DeleteApplicationByIdRequestDataValidator()
    {
        RuleFor(request => request.DocumentId)
            .NotEmpty();
    }
}