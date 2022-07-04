using AutoMapper;
using ITCompanyCVManager.Boundary.Context.Common;
using ITCompanyCVManager.Boundary.Context.User;
using ITCompanyCVManager.Domain.ElasticIndex;
using ITCompanyCVManager.Domain.Services.Models;

namespace ITCompanyCVManager.Api.Components.Mapping;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<ApplicationResponse, Application>()
            .ForPath(dest => dest.ApplicantFirstname,
                opt => { opt.MapFrom(src => src.ApplicantFirstname); })
            .ForPath(dest => dest.ApplicantLastname,
                opt => { opt.MapFrom(src => src.ApplicantLastname); })
            .ForPath(dest => dest.ApplicantPhone,
                opt => { opt.MapFrom(src => src.ApplicantPhone); })
            .ForPath(dest => dest.ApplicantEmail,
                opt => { opt.MapFrom(src => src.ApplicantEmail); })
            .ForPath(dest => dest.CityName,
                opt => { opt.MapFrom(src => src.CityName); })
            .ForPath(dest => dest.CvFileName,
                opt => { opt.MapFrom(src => src.CvFileName); })
            .ForPath(dest => dest.CvContent,
                opt => { opt.MapFrom(src => src.CvContent); })
            .ForPath(dest => dest.CoverLetterContent,
                opt => { opt.MapFrom(src => src.CoverLetterContent); })
            .ForPath(dest => dest.CoverLetterFileName,
                opt => { opt.MapFrom(src => src.CoverLetterFileName); })
            .ForPath(dest => dest.DateCreated,
                opt => { opt.MapFrom(src => src.DateCreated); })
            .ForPath(dest => dest.ApplicantEducationLevel,
                opt => { opt.MapFrom(src => src.ApplicantEducationLevel); })
            .ForPath(dest => dest.Id,
                opt => { opt.MapFrom(src => src.Id); })
            .ReverseMap();

        CreateMap<ApplicationSearchResponse, Application>()
            .ForPath(dest => dest.ApplicantFirstname,
                opt => { opt.MapFrom(src => src.ApplicantFirstname); })
            .ForPath(dest => dest.ApplicantLastname,
                opt => { opt.MapFrom(src => src.ApplicantLastname); })
            .ForPath(dest => dest.ApplicantPhone,
                opt => { opt.MapFrom(src => src.ApplicantPhone); })
            .ForPath(dest => dest.ApplicantEmail,
                opt => { opt.MapFrom(src => src.ApplicantEmail); })
            .ForPath(dest => dest.CityName,
                opt => { opt.MapFrom(src => src.CityName); })
            .ForPath(dest => dest.CvFileName,
                opt => { opt.MapFrom(src => src.CvFileName); })
            .ForPath(dest => dest.CvContent,
                opt => { opt.MapFrom(src => src.CvContent); })
            .ForPath(dest => dest.CoverLetterContent,
                opt => { opt.MapFrom(src => src.CoverLetterContent); })
            .ForPath(dest => dest.CoverLetterFileName,
                opt => { opt.MapFrom(src => src.CoverLetterFileName); })
            .ForPath(dest => dest.DateCreated,
                opt => { opt.MapFrom(src => src.DateCreated); })
            .ForPath(dest => dest.ApplicantEducationLevel,
                opt => { opt.MapFrom(src => src.ApplicantEducationLevel); })
            .ForPath(dest => dest.Id,
                opt => { opt.MapFrom(src => src.Id); })
            .ReverseMap();

        CreateMap<ResultWithHighlightsResponse, ResultWithHighlights>()
            .ForPath(dest => dest.Application.ApplicantFirstname,
                opt => { opt.MapFrom(src => src.Application.ApplicantFirstname); })
            .ForPath(dest => dest.Application.ApplicantLastname,
                opt => { opt.MapFrom(src => src.Application.ApplicantLastname); })
            .ForPath(dest => dest.Application.ApplicantPhone,
                opt => { opt.MapFrom(src => src.Application.ApplicantPhone); })
            .ForPath(dest => dest.Application.ApplicantEmail,
                opt => { opt.MapFrom(src => src.Application.ApplicantEmail); })
            .ForPath(dest => dest.Application.CityName,
                opt => { opt.MapFrom(src => src.Application.CityName); })
            .ForPath(dest => dest.Application.CvFileName,
                opt => { opt.MapFrom(src => src.Application.CvFileName); })
            .ForPath(dest => dest.Application.CvContent,
                opt => { opt.MapFrom(src => src.Application.CvContent); })
            .ForPath(dest => dest.Application.CoverLetterContent,
                opt => { opt.MapFrom(src => src.Application.CoverLetterContent); })
            .ForPath(dest => dest.Application.CoverLetterFileName,
                opt => { opt.MapFrom(src => src.Application.CoverLetterFileName); })
            .ForPath(dest => dest.Application.DateCreated,
                opt => { opt.MapFrom(src => src.Application.DateCreated); })
            .ForPath(dest => dest.Application.ApplicantEducationLevel,
                opt => { opt.MapFrom(src => src.Application.ApplicantEducationLevel); })
            .ForPath(dest => dest.Application.Id,
                opt => { opt.MapFrom(src => src.Application.Id); })
            .ReverseMap();

        CreateMap<ResultWithHighlights, ResultWithHighlightsResponse>()
            .ForPath(dest => dest.Application.ApplicantFirstname,
                opt => { opt.MapFrom(src => src.Application.ApplicantFirstname); })
            .ForPath(dest => dest.Application.ApplicantLastname,
                opt => { opt.MapFrom(src => src.Application.ApplicantLastname); })
            .ForPath(dest => dest.Application.ApplicantPhone,
                opt => { opt.MapFrom(src => src.Application.ApplicantPhone); })
            .ForPath(dest => dest.Application.ApplicantEmail,
                opt => { opt.MapFrom(src => src.Application.ApplicantEmail); })
            .ForPath(dest => dest.Application.CityName,
                opt => { opt.MapFrom(src => src.Application.CityName); })
            .ForPath(dest => dest.Application.CvFileName,
                opt => { opt.MapFrom(src => src.Application.CvFileName); })
            .ForPath(dest => dest.Application.CvContent,
                opt => { opt.MapFrom(src => src.Application.CvContent); })
            .ForPath(dest => dest.Application.CoverLetterContent,
                opt => { opt.MapFrom(src => src.Application.CoverLetterContent); })
            .ForPath(dest => dest.Application.CoverLetterFileName,
                opt => { opt.MapFrom(src => src.Application.CoverLetterFileName); })
            .ForPath(dest => dest.Application.DateCreated,
                opt => { opt.MapFrom(src => src.Application.DateCreated); })
            .ForPath(dest => dest.Application.ApplicantEducationLevel,
                opt => { opt.MapFrom(src => src.Application.ApplicantEducationLevel); })
            .ForPath(dest => dest.Application.Id,
                opt => { opt.MapFrom(src => src.Application.Id); })
            .ReverseMap();
        CreateMap<ApplicationSearchResponse, ApplicationResponse>()
            .ForPath(dest => dest.ApplicantFirstname,
                opt => { opt.MapFrom(src => src.ApplicantFirstname); })
            .ForPath(dest => dest.ApplicantLastname,
                opt => { opt.MapFrom(src => src.ApplicantLastname); })
            .ForPath(dest => dest.ApplicantPhone,
                opt => { opt.MapFrom(src => src.ApplicantPhone); })
            .ForPath(dest => dest.ApplicantEmail,
                opt => { opt.MapFrom(src => src.ApplicantEmail); })
            .ForPath(dest => dest.CityName,
                opt => { opt.MapFrom(src => src.CityName); })
            .ForPath(dest => dest.CvFileName,
                opt => { opt.MapFrom(src => src.CvFileName); })
            .ForPath(dest => dest.CvContent,
                opt => { opt.MapFrom(src => src.CvContent); })
            .ForPath(dest => dest.CoverLetterContent,
                opt => { opt.MapFrom(src => src.CoverLetterContent); })
            .ForPath(dest => dest.CoverLetterFileName,
                opt => { opt.MapFrom(src => src.CoverLetterFileName); })
            .ForPath(dest => dest.DateCreated,
                opt => { opt.MapFrom(src => src.DateCreated); })
            .ForPath(dest => dest.ApplicantEducationLevel,
                opt => { opt.MapFrom(src => src.ApplicantEducationLevel); })
            .ForPath(dest => dest.Id,
                opt => { opt.MapFrom(src => src.Id); })
            .ReverseMap();

        CreateMap<CreateApplicationRequest, Application>().ReverseMap();
    }
}