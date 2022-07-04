using System.Net;
using ITCompanyCVManager.Business.Exceptions;
using ITCompanyCVManager.Domain.Services;
using ITCompanyCVManager.Domain.Services.Models;
using ITCompanyCVManager.Services.Implementation.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace ITCompanyCVManager.Services.Implementation;

public class GeoLocationDecodeService : 
    IGeoLocationDecodeService
{
    private readonly IConfiguration _configuration;
    public GeoLocationDecodeService(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }
    public async Task<CityGeoLocation> DecodeCityLatLong(string cityName)
    {
        var decodeUrl = _configuration["geolocation:decodeUrl"];
        var apiKey = _configuration["geolocation:key"];
        if (apiKey == null)
        {
            throw new ServiceUnavailableException();
        }

        var fullUrl = string.Format(decodeUrl, apiKey, cityName);
        using var client = new RestClient();
        var restRequest = new RestRequest($"{fullUrl}", Method.Get);
        var result = await client.ExecuteAsync<CityDecodeResult>(restRequest).ConfigureAwait(false);
        client.Dispose();

        if (result.StatusCode == HttpStatusCode.OK)
        {
            if (result.Data is not null)
            {
                var data = result.Data.Data.FirstOrDefault();
                if (data != null)
                {
                    return new CityGeoLocation
                    {
                        Name = data.Type == "country" ? "Serbia" : cityName,
                        Latitude = data.Latitude,
                        Longitude = data.Longitude
                    };
                }
               
            }

            throw new CityNotFoundException(cityName);
        }
        else if (result.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new CityNotFoundException(cityName);
        }
        else if (result.StatusCode == HttpStatusCode.NotFound)
        {
            throw new CityNotFoundException(cityName);
        }
        else
        {
            throw new CityNotFoundException(cityName);
        }
    }

    public async Task<CityGeoLocation> DecodeCityName(double latitude, double longitude)
    {
        var decodeUrl = _configuration["geolocation:reDecodeUrl"];
        var apiKey = _configuration["geolocation:key"];
        if (apiKey == null)
        {
            throw new ServiceUnavailableException();
        }

        var fullUrl = string.Format(decodeUrl, apiKey, latitude, longitude);
        using var client = new RestClient();
        var restRequest = new RestRequest($"{fullUrl}", Method.Get);
        var result = await client.ExecuteAsync<CityDecodeResult>(restRequest).ConfigureAwait(false);
        client.Dispose();

        if (result.StatusCode == HttpStatusCode.OK)
        {
            if (result.Data is not null)
            {
                var data = result.Data.Data.FirstOrDefault();
                if (data != null)
                {
                    return new CityGeoLocation
                    {
                        Name = data.Name,
                        Latitude = data.Latitude,
                        Longitude = data.Longitude
                    };
                }
            }

            throw new CityNotFoundException();
        }
        else if (result.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new CityNotFoundException();
        }
        else if (result.StatusCode == HttpStatusCode.NotFound)
        {
            throw new CityNotFoundException();
        }
        else
        {
            throw new CityNotFoundException();
        }
    }
}