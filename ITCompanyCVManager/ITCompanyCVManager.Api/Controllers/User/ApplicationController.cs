using System.Net;
using AutoWrapper.Wrappers;
using ITCompanyCVManager.Api.Controllers.Base;
using ITCompanyCVManager.Boundary.Context.Admin;
using ITCompanyCVManager.Boundary.Context.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITCompanyCVManager.Api.Controllers.User;

/// <summary>
/// Data flows for address
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ApplicationController :
    ApiControllerBase
{
    public ApplicationController(ISender mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Validate address.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost("create")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateApplicationResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ApiResponse> ValidateAddress([FromForm] CreateApplicationRequest request, CancellationToken token = default)
    {
        var response = await Mediator.Send(request, token);
        return new ApiResponse(response);
    }
}