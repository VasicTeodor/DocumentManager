using MediatR;

namespace ITCompanyCVManager.Boundary.Context.Admin;

public record DownloadCvByIdRequest :
    IRequest<DownloadCvByIdResponse>
{
    public Guid DocumentId { get; set; }
}

public record DownloadCvByIdResponse
{
    public FileStream CvContent { get; set; }
    public string CvName { get; set; }
}