namespace ITCompanyCVManager.Domain.Exceptions.Status;

public class NotFoundException :
    ApplicationException
{
    protected NotFoundException(string title, string message, ErrorCode code = default)
        : base(title, message, code ?? ErrorCode.NotFound)
    {
    }

    protected NotFoundException(string title, string message, Exception exception, ErrorCode code = default)
        : base(title, message, exception, code ?? ErrorCode.NotFound)
    {
    }
}