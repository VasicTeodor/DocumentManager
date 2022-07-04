using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ITCompanyCVManager.Api.Configuration;

public class NewtonsoftJsonOptionsConfigurator :
    IConfigureOptions<MvcNewtonsoftJsonOptions>
{
    public void Configure(MvcNewtonsoftJsonOptions options)
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
        options.SerializerSettings.DateFormatString = "yyyy-MM-ddTHH:mm:ssZ";
    }
}