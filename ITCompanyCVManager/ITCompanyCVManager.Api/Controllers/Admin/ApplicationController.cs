using System.Net;
using AutoWrapper.Filters;
using AutoWrapper.Wrappers;
using ITCompanyCVManager.Api.Controllers.Base;
using ITCompanyCVManager.Boundary.Context.Admin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITCompanyCVManager.Api.Controllers.Admin;

/// <summary>
/// Data flows for address
/// </summary>
[Authorize]
[ApiController]
[Area("admin")]
[Route("api/[area]/[controller]")]
public class ApplicationController :
    ApiControllerBase
{
    public ApplicationController(ISender mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Search applications by firstname and lastname.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("search-by-name")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SearchDocumentsByApplicantNameResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ApiResponse> SearchByName([FromQuery] SearchDocumentsByApplicantNameRequest request,
        CancellationToken token = default)
    {
        var response = await Mediator.Send(request, token);
        return new ApiResponse(response);
    }

    /// <summary>
    /// Search applications by applicant education level.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("search-by-education-level")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SearchApplicationsByApplicantEducationLevelResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ApiResponse> SearchByEducationLevel([FromQuery] SearchApplicationsByApplicantEducationLevelRequest request,
        CancellationToken token = default)
    {
        var response = await Mediator.Send(request, token);
        return new ApiResponse(response);
    }

    /// <summary>
    /// Search applications by applicant cover letter content.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("search-by-cover-letter")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SearchApplicantsByCoverLetterContentResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ApiResponse> SearchByCoverLetterContent([FromQuery] SearchApplicantsByCoverLetterContentRequest request,
        CancellationToken token = default)
    {
        var response = await Mediator.Send(request, token);
        return new ApiResponse(response);
    }

    /// <summary>
    /// Get all applications.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetAllApplicationsResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ApiResponse> GetAll([FromQuery] GetAllApplicationsRequest request,
        CancellationToken token = default)
    {
        var response = await Mediator.Send(request, token);
        return new ApiResponse(response);
    }

    /// <summary>
    /// Index test data.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("index-test-data")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ApiResponse> IndexTestData(CancellationToken token = default)
    {
        var request = new IndexTestDataRequest();
        var response = await Mediator.Send(request, token);
        return new ApiResponse(response);
    }

    /// <summary>
    /// Delete document by id.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpDelete("{documentId:guid}")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ApiResponse> DeleteDocumentById([FromRoute] DeleteApplicationByIdRequest request, CancellationToken token = default)
    {
        var response = await Mediator.Send(request, token);
        return new ApiResponse(response);
    }

    /// <summary>
    /// Search applications by phrase.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("search-by-phrase")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SearchApplicantsByPhraseResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ApiResponse> SearchByEducationLevel([FromQuery] SearchApplicantsByPhraseRequest request,
        CancellationToken token = default)
    {
        var response = await Mediator.Send(request, token);
        return new ApiResponse(response);
    }

    /// <summary>
    /// Search applications by bool query.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("search-by-bool-query")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SearchByCombinedParametersResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ApiResponse> SearchByBoolQuery([FromQuery] SearchByCombinedParametersRequest request,
        CancellationToken token = default)
    {
        var response = await Mediator.Send(request, token);
        return new ApiResponse(response);
    }

    /// <summary>
    /// Search applicants by radius of city.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("search-by-radius")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetApplicantsByGeoLocationResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ApiResponse> SearchByCityRadius([FromQuery] GetApplicantsByGeoLocationRequest request,
        CancellationToken token = default)
    {
        var response = await Mediator.Send(request, token);
        return new ApiResponse(response);
    }

    /// <summary>
    /// Search applicants by radius of city.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [AutoWrapIgnore]
    [HttpGet("download-cv/{documentId:guid}")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DownloadCvByIdResponse))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetApplicantCv([FromRoute] DownloadCvByIdRequest request, CancellationToken token = default)
    {
        var response = await Mediator.Send(request, token);
        return File(response.CvContent, "application/octet-stream", response.CvName);
    }
}