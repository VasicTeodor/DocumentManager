namespace ITCompanyCVManager.Domain.Exceptions.Status;

public class UnprocessableEntityException :
    ApplicationException
{
    protected UnprocessableEntityException(string title, string message, ErrorCode code = default)
        : base(title, message, code ?? ErrorCode.UnprocessableEntity)
    {
    }

    protected UnprocessableEntityException(string title, string message, Exception exception, ErrorCode code = default)
        : base(title, message, exception, ErrorCode.UnprocessableEntity)
    {
    }
}