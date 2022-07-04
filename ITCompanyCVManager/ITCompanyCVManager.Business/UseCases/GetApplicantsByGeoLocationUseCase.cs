using AutoMapper;
using ITCompanyCVManager.Boundary.Context.Admin;
using ITCompanyCVManager.Boundary.Context.Common;
using ITCompanyCVManager.Domain.ElasticIndex;
using ITCompanyCVManager.Domain.Services;
using MediatR;
using Nest;

namespace ITCompanyCVManager.Business.UseCases;

public class GetApplicantsByGeoLocationUseCase :
    IRequestHandler<GetApplicantsByGeoLocationRequest, GetApplicantsByGeoLocationResponse>
{
    private readonly IElasticClient _elasticClient;
    private readonly IMapResponseWithHighlightsService _mapResponseWithHighlightsService;
    private readonly IMapper _mapper;
    private readonly IGeoLocationDecodeService _geoLocationDecodeService;

    public GetApplicantsByGeoLocationUseCase(IElasticClient elasticClient,
        IMapper mapper,
        IMapResponseWithHighlightsService mapResponseWithHighlightsService,
        IGeoLocationDecodeService geoLocationDecodeService)
    {
        _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mapResponseWithHighlightsService = mapResponseWithHighlightsService ??
                                            throw new ArgumentNullException(nameof(mapResponseWithHighlightsService));
        _geoLocationDecodeService = geoLocationDecodeService ?? throw new ArgumentNullException(nameof(geoLocationDecodeService));
    }
    public async Task<GetApplicantsByGeoLocationResponse> Handle(GetApplicantsByGeoLocationRequest request, CancellationToken cancellationToken)
    {
        var city = await _geoLocationDecodeService.DecodeCityLatLong(request.City);

        var searchResponse = await _elasticClient.SearchAsync<Application>(
            s => s.Query(q => q
                .GeoDistance(g => g
                    .Boost(1.1)
                    .Name("geo_query")
                    .Field(p => p.LatLongLocation)
                    .DistanceType(GeoDistanceType.Arc)
                    .Location(city.Latitude, city.Longitude)
                    .Distance(request.Radius, DistanceUnit.Kilometers)
                    .ValidationMethod(GeoValidationMethod.IgnoreMalformed)
                )), cancellationToken);

        var searchResults =
            _mapper.Map<List<ResultWithHighlightsResponse>>(_mapResponseWithHighlightsService.Map(searchResponse));

        var result = new GetApplicantsByGeoLocationResponse
        {
            SearchResults = searchResults
        };

        return result;
    }
}