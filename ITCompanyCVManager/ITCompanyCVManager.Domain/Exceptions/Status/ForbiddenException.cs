namespace ITCompanyCVManager.Domain.Exceptions.Status;

public class ForbiddenException :
    ApplicationException
{
    protected ForbiddenException(string title, string message, ErrorCode code = default)
        : base(title, message, code ?? ErrorCode.Forbidden)
    {
    }

    protected ForbiddenException(string title, string message, Exception exception, ErrorCode code = default)
        : base(title, message, exception, code ?? ErrorCode.Forbidden)
    {
    }
}