using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITCompanyCVManager.Api.Controllers.Base;

public class ApiControllerBase :
    ControllerBase
{
    protected readonly ISender Mediator;

    public ApiControllerBase(ISender mediator)
    {
        Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
}