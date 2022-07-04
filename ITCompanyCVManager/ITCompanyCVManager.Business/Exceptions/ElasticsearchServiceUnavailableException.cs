using ITCompanyCVManager.Domain.Exceptions;

namespace ITCompanyCVManager.Business.Exceptions;

public class ElasticsearchServiceUnavailableException :
    Domain.Exceptions.Status.ServiceUnavailableException
{
    private const string TITLE = "Elasticsearch service unavailable";
    private const string MESSAGE = "Check your connection";

    public ElasticsearchServiceUnavailableException()
        : base(TITLE, MESSAGE, ErrorCode.ElasticsearchServiceUnavailable)
    {
    }
}