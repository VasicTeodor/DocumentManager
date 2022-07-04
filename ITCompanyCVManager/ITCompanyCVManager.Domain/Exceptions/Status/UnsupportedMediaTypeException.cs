namespace ITCompanyCVManager.Domain.Exceptions.Status;

public class UnsupportedMediaTypeException :
    ApplicationException
{
    public UnsupportedMediaTypeException(string title, string message, ErrorCode code = default)
        : base(title, message, code ?? ErrorCode.UnsupportedMediaType)
    {
    }

    public UnsupportedMediaTypeException(string title, string message, Exception exception, ErrorCode code = default)
        : base(title, message, exception, code ?? ErrorCode.UnsupportedMediaType)
    {
    }
}