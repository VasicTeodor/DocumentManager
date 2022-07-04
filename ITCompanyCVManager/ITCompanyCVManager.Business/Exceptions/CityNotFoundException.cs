using ITCompanyCVManager.Domain.Exceptions;
using ITCompanyCVManager.Domain.Exceptions.Status;

namespace ITCompanyCVManager.Business.Exceptions;

public class CityNotFoundException :
    NotFoundException
{
    private const string TITLE = "City not found";
    private const string MESSAGE = "City {0} in job application is not found, please chose some city near yours";
    private const string MESSAGE_LAT_LONG = "City with given latitude and longitude not found";

    public CityNotFoundException(string city)
        : base(TITLE, string.Format(MESSAGE, city), ErrorCode.CityNotFound)
    {
    }

    public CityNotFoundException()
        : base(TITLE, MESSAGE_LAT_LONG, ErrorCode.CityNotFound)
    {
    }
}