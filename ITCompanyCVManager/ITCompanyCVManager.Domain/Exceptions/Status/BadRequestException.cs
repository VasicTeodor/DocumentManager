namespace ITCompanyCVManager.Domain.Exceptions.Status;

public class BadRequestException :
    ApplicationException
{
    protected BadRequestException(string title, string message, ErrorCode code = default)
        : base(title, message, code ?? ErrorCode.BadRequest)
    {
    }

    protected BadRequestException(string title, string message, Exception exception, ErrorCode code = default)
        : base(title, message, exception, code ?? ErrorCode.BadRequest)
    {
    }
}