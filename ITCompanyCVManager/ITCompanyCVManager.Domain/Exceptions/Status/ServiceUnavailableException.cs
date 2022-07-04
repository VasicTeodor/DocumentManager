namespace ITCompanyCVManager.Domain.Exceptions.Status;

public class ServiceUnavailableException :
    ApplicationException
{
    public ServiceUnavailableException(string title, string message, ErrorCode code = default)
        : base(title, message, code ?? ErrorCode.ServiceUnavailable)
    {
    }

    public ServiceUnavailableException(string title, string message, Exception exception, ErrorCode code = default)
        : base(title, message, exception, code ?? ErrorCode.ServiceUnavailable)
    {
    }
}