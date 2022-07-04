namespace ITCompanyCVManager.Domain.Exceptions.Status;

public class InternalServerErrorException :
    ApplicationException
{
    public InternalServerErrorException(string title, string message, ErrorCode code = default)
        : base(title, message, code ?? ErrorCode.Internal)
    {
    }

    public InternalServerErrorException(string title, string message, Exception exception, ErrorCode code = default)
        : base(title, message, exception, code ?? ErrorCode.Internal)
    {
    }
}