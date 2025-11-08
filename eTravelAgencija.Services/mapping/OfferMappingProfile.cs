using AutoMapper;
using eTravelAgencija.Model;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;
using eTravelAgencija.Services.Database;

public class OfferMappingProfile : Profile
{
    public OfferMappingProfile()
    {
        // Entitet -> Response
        CreateMap<Offer, OfferAdminResponse>()
            .ForMember(dest => dest.OfferName, opt => opt.MapFrom(src => src.Title));

        CreateMap<Offer, OfferUserResponce>()
            .ForMember(dest => dest.OfferName, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.SubCategory))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.SubCategory.Category));

        CreateMap<OfferSubCategory, OfferSubCategoryResponse>()
    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

        CreateMap<OfferCategory, OfferCategoryResponse>();

        CreateMap<OfferDetails, OfferAdminDetailResponse>()
            .ForMember(dest => dest.County, opt => opt.MapFrom(src => src.Country));

        CreateMap<OfferDetails, OfferUserDetailResponse>();

        CreateMap<Offer, OfferUpsertResponse>()
            .ForMember(dest => dest.OfferName, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Details.Country))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Details.City))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Details.Description));

        // Request -> Entitet (za kreiranje/izmjenu)
        CreateMap<OfferRequest, Offer>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategoryId == 0 ? -1 : src.SubCategoryId))
            .ForMember(dest => dest.Details, opt => opt.MapFrom(src => new OfferDetails
            {
                Description = src.Description,
                Country = src.Country,
                City = src.City
            }));
    }
}
