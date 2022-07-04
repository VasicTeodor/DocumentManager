using ITCompanyCVManager.Domain.ElasticIndex;
using Nest;

namespace ITCompanyCVManager.Persistence.Helpers;

public static class IndexTestData
{
    public static List<Application> GetTestData()
    {
        List<Application> unit = new List<Application>();

        unit.Add(new Application
        {
            Id = Guid.NewGuid(),
            ApplicantFirstname = "Donatelo",
            ApplicantLastname = "Aligleri",
            ApplicantEmail = "don@aligleri.com",
            ApplicantPhone = "(020) 123-1231",
            ApplicantEducationLevel = 6,
            CvContent = "Backend developer interested in Java technologies.",
            CoverLetterContent = @"I am Donatelo Aligleri i have worked with technologies such as Java, tomcat, c#, .net, visual studio, java script, Angular. Also i know thing or two about dev ops, docker, kubernates, aws and azure.",
            CvFileName = "DanteCV.pdf",
            CoverLetterFileName = "CoverLetter.pdf",
            LatLongLocation = new GeoLocation(44.833, 20.500)
        });

        unit.Add(new Application
        {
            Id = Guid.NewGuid(),
            ApplicantFirstname = "Laslo",
            ApplicantLastname = "Kraus",
            ApplicantEmail = "laslo@gmail.com",
            ApplicantPhone = "(020) 123-4567",
            ApplicantEducationLevel = 6,
            CoverLetterContent = "Moje ime je Laslo Kraus. Iskusni sam programer vec duzi niz godina, a tehnologije u kojima sam radio su sledece: .Net, Angular, Jaa, c#, java srcipt. Koristio sam visual studio i visual studio code.",
            CvContent = "Iskusni backend programer sa visegodisnjim iskustvom",
            CvFileName = "LasloKCV.pdf",
            CoverLetterFileName = "PopratnoPismo.pdf",
            LatLongLocation = new GeoLocation(45.830, 20.462)
        });

        unit.Add(new Application
        {
            Id = Guid.NewGuid(),
            ApplicantFirstname = "Franc",
            ApplicantLastname = "Kafka",
            ApplicantEmail = "fkafka@gmail.com",
            ApplicantPhone = "(020) 123-1356",
            CoverLetterContent = "Veliki strastveni front end programer, koji uživa u pisanju java script koda. Od frejmvorka sam koristio Angular i React. Volim kučiće i mačiće pa se nadam da je firma pet friendly.",
            ApplicantEducationLevel = 1,
            CvContent = "Front end programer koji živi život punim plućima",
            CvFileName = "FrKfCV.pdf",
            CoverLetterFileName = "Popratno.pdf",
            LatLongLocation = new GeoLocation(44.8125, 20.4612)
        });

        unit.Add(new Application
        {
            Id = Guid.NewGuid(),
            ApplicantFirstname = "Галанз",
            ApplicantLastname = "Дел",
            ApplicantEmail = "galanzd@gmail.com",
            ApplicantPhone = "(020) 123-4567",
            ApplicantEducationLevel = 2,
            CoverLetterContent = "Добар сам програмер, радио сам на доста пројеката где сам користио Ангулар, јава сцрипт, Јаву, Реакт, Ангулар и девопс технологије. Дружељубиљив сам човек и волим друштво и кучиће и мачиће.",
            CvContent = "Солидан програмер са добрим знањем солида.",
            CvFileName = "ПрограмерГаланз.pdf",
            CoverLetterFileName = "ППисмо.pdf",
            LatLongLocation = new GeoLocation(44.7553, 19.6923)
        });

        return unit;

    }
}