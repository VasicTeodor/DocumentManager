namespace ITCompanyCVManager.Domain.Exceptions;

public abstract class ApplicationException :
    Exception
{
    public string Title { get; }
    public ErrorCode Code { get; }

    protected ApplicationException(string title, string message, ErrorCode code = default) :
        base(message)
    {
        Title = title;
        Code = code ?? ErrorCode.Default;
    }

    protected ApplicationException(string title, string message, Exception exception, ErrorCode code = default) :
        base(message, exception)
    {
        Title = title;
        Code = code ?? ErrorCode.Default;
    }
}