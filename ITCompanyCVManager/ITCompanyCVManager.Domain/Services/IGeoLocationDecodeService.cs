using ITCompanyCVManager.Domain.Services.Models;

namespace ITCompanyCVManager.Domain.Services;

public interface IGeoLocationDecodeService
{
    Task<CityGeoLocation> DecodeCityLatLong(string cityName);
    Task<CityGeoLocation> DecodeCityName(double latitude, double longitude);
}