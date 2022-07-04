namespace ITCompanyCVManager.Domain.Exceptions.Status;

public class ServiceResponseErrorException :
    ApplicationException
{
    public ServiceResponseErrorException(string title, string message, ErrorCode code = default)
        : base(title, message, code ?? ErrorCode.ServiceResponseError)
    {
    }

    public ServiceResponseErrorException(string title, string message, Exception exception, ErrorCode code = default)
        : base(title, message, exception, code ?? ErrorCode.ServiceResponseError)
    {
    }
}