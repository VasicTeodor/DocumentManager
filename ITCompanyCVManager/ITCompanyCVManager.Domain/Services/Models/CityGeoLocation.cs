namespace ITCompanyCVManager.Domain.Services.Models;

public record CityGeoLocation
{
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}