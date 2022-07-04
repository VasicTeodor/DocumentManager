using ITCompanyCVManager.Domain.ElasticIndex;
using Nest;

namespace ITCompanyCVManager.Persistence.ElasticSearchIndexSettings;

public static class ElasticSearchMappings
{
    public static void DefaultMappings(ConnectionSettings settings)
    {
        settings.DefaultMappingFor<Application>(mapping => mapping.IdProperty(x => x.Id));
    }

    public static void CreateApplicationIndex(IElasticClient client, string indexName)
    {
        var createIndexResponse = client.Indices.Create(indexName,
            c => c
                .Map<Application>(mapping => mapping
                    .Properties(properties => properties
                        .Text(textProperty => textProperty
                            .Name(name => name.ApplicantFirstname)
                            .Analyzer("serbian"))
                        .Date(dateProperty => dateProperty
                            .Name(name => name.DateCreated))
                        .Text(textProperty => textProperty
                            .Name(name => name.ApplicantLastname)
                            .Analyzer("serbian"))
                        .Text(textProperty => textProperty
                            .Name(name => name.CvContent)
                            .Analyzer("serbian"))
                        .Keyword(textProperty => textProperty
                            .Name(name => name.CityName))
                        .Text(textProperty => textProperty
                            .Name(name => name.CvFileName)
                            .Analyzer("serbian"))
                        .Text(textProperty => textProperty
                            .Name(name => name.CoverLetterFileName)
                            .Analyzer("serbian"))
                        .Text(textProperty => textProperty
                            .Name(name => name.CoverLetterContent)
                            .Analyzer("serbian"))
                        .Text(textProperty => textProperty
                            .Name(name => name.ApplicantPhone))
                        .Text(textProperty => textProperty
                            .Name(name => name.ApplicantEmail))
                        .Number(numberProperty => numberProperty
                            .Name(name => name.ApplicantEducationLevel))
                        .GeoPoint(geoLatLongProperty => geoLatLongProperty
                            .Name(name => name.LatLongLocation)
                        ))));
    }
}