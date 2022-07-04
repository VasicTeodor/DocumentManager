using Microsoft.Extensions.Options;

namespace ITCompanyCVManager.Api.Configuration;

public class RouteOptionsConfigurator :
    IConfigureOptions<RouteOptions>
{
    public void Configure(RouteOptions options)
    {
        options.LowercaseUrls = true;
    }
}