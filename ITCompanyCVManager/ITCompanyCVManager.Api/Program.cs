using AutoMapper;
using AutoWrapper;
using FluentValidation;
using ITCompanyCVManager.Api.Components.Mapping;
using ITCompanyCVManager.Api.Configuration;
using ITCompanyCVManager.Api.PipelineBehavior;
using ITCompanyCVManager.Domain.Services;
using ITCompanyCVManager.Services.Implementation;
using MediatR;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File($@"Logs\Api.log")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

//AddElasticSearch
builder.Services.ConfigureElasticSearch(builder.Configuration);

// Add services to the container.
builder.Services.ConfigureOptions<RouteOptionsConfigurator>();
builder.Services.ConfigureOptions<NewtonsoftJsonOptionsConfigurator>();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var mediatorScanAssemblies = new[]
{
    typeof(ITCompanyCVManager.Business.AssemblyReference).Assembly
};

builder.Services.AddMediatR(mediatorScanAssemblies);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationRequestBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add validator
builder.Services.AddValidatorsFromAssembly(typeof(ITCompanyCVManager.Boundary.AssemblyReference).Assembly);

// Add services
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IMapResponseWithHighlightsService, MapResponseWithHighlightsService>();
builder.Services.AddScoped<IGeoLocationDecodeService, GeoLocationDecodeService>();

// Register Mapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfiles());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsBuilder => corsBuilder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
{
    UseApiProblemDetailsException = true,
    ApiVersion = "1.0",
    ShowApiVersion = true
});

app.MapControllers();

app.Run();