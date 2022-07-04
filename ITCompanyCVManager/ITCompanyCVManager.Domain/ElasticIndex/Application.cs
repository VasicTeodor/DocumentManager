using Nest;

namespace ITCompanyCVManager.Domain.ElasticIndex;

public class Application
{
    public Guid Id { get; set; }
    public string ApplicantFirstname { get; set; }
    public string ApplicantLastname { get; set; }
    public string ApplicantPhone { get; set; }
    public string ApplicantEmail { get; set; }
    public int ApplicantEducationLevel { get; set; }
    public string CityName { get; set; }
    public string CvContent { get; set; }
    public string CoverLetterContent { get; set; }
    public string CvFileName { get; set; }
    public string CoverLetterFileName { get; set; }
    public DateTime DateCreated { get; set; }
    public GeoLocation LatLongLocation { get; set; }
}