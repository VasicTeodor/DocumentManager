namespace ITCompanyCVManager.Domain.Exceptions;

public class ErrorCode
{
    public int Code { get; }
    public string Description { get; }

    private ErrorCode(int code, string description)
    {
        Code = code;
        Description = description;
    }

    public override string ToString()
    {
        return Code.ToString();
    }


    public static ErrorCode ClientRequestDataValidationError => new(111, "Client request data validation failure");
    public static ErrorCode Default => new(1, "Default code");

    public static ErrorCode Internal => new(2, "Internal");
    public static ErrorCode NotFound => new(3, "Not found");
    public static ErrorCode BadRequest => new(4, "Bad request");
    public static ErrorCode Forbidden => new(5, "Bad request");
    public static ErrorCode ServiceUnavailable => new(6, "Service unavailable");
    public static ErrorCode UnsupportedMediaType => new(7, "Unsupported media type");
    public static ErrorCode UnprocessableEntity => new(8, "Unprocessable entity");
    public static ErrorCode Server => new(9, "Server unhandled error");
    public static ErrorCode ServiceResponseError => new(10, "Service response error");
    public static ErrorCode CityNotFound => new(11, "City not found");
    public static ErrorCode GeoDecodingServiceUnavailable => new(12, "Geo decoding service unavailable");
    public static ErrorCode ElasticsearchServiceUnavailable => new(13, "Elasticsearch service unavailable");
}