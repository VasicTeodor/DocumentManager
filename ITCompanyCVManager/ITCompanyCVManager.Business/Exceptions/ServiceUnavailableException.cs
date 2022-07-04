using ITCompanyCVManager.Domain.Exceptions;

namespace ITCompanyCVManager.Business.Exceptions;

public class ServiceUnavailableException :
    Domain.Exceptions.Status.ServiceUnavailableException
{
    private const string TITLE = "Geo decoding service unavailable";
    private const string MESSAGE = "Check your api key";

    public ServiceUnavailableException()
        : base(TITLE, MESSAGE, ErrorCode.GeoDecodingServiceUnavailable)
    {
    }
}