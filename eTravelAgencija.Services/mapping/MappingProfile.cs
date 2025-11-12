using AutoMapper;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Model.ResponseObjects;
using System.Linq;
using eTravelAgencija.Model.Requests;
using System;

namespace eTravelAgencija.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 🟢 OfferInsertRequest → Offer (Database)
            CreateMap<OfferInsertRequest, Offer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID se generiše u bazi
                .ForMember(dest => dest.Details, opt => opt.Ignore()) // kreira se ručno u BeforeInsertAsync
                .ForMember(dest => dest.SubCategory, opt => opt.Ignore()); // ako se koristi FK samo

            // 🟡 OfferUpdateRequest → Offer (Database)
            CreateMap<OfferUpdateRequest, Offer>()
              .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
              .ForMember(dest => dest.DaysInTotal, opt => opt.MapFrom(src => src.DaysInTotal))
              .ForMember(dest => dest.WayOfTravel, opt => opt.MapFrom(src => src.WayOfTravel))
              .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategoryId))
              .ForMember(dest => dest.Details, opt => opt.Ignore())
              .ForMember(dest => dest.SubCategory, opt => opt.Ignore());

            // 🔹 Iz Offer entiteta u Request DTO (npr. za edit formu u frontendu)
            CreateMap<Offer, OfferUpdateRequest>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.DaysInTotal, opt => opt.MapFrom(src => src.DaysInTotal))
                .ForMember(dest => dest.WayOfTravel, opt => opt.MapFrom(src => src.WayOfTravel))
                .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategoryId))
                // 🔸 Polja koja dolaze iz Offer.Details
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Details.Description))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Details.Country))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Details.City))
                .ForMember(dest => dest.MinimalPrice, opt => opt.MapFrom(src => src.Details.MinimalPrice))
                .ForMember(dest => dest.TravelInsuranceTotal, opt => opt.MapFrom(src => src.Details.TravelInsuranceTotal))
                .ForMember(dest => dest.ResidenceTotal, opt => opt.MapFrom(src => src.Details.ResidenceTotal))
                .ForMember(dest => dest.ResidenceTaxPerDay, opt => opt.MapFrom(src => src.Details.ResidenceTaxPerDay));

            // 🔵 Offer (Database) → Offer (Model)
            CreateMap<Offer, Model.model.Offer>()
                .ForMember(dest => dest.OfferId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.DaysInTotal, opt => opt.MapFrom(src => src.DaysInTotal))
                .ForMember(dest => dest.WayOfTravel, opt => opt.MapFrom(src => src.WayOfTravel))
                .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategoryId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Details.Description))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Details.Country))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Details.City))
                .ForMember(dest => dest.MinimalPrice, opt => opt.MapFrom(src => src.Details.MinimalPrice))
                .ForMember(dest => dest.ResidenceTaxPerDay, opt => opt.MapFrom(src => src.Details.ResidenceTaxPerDay))
                .ForMember(dest => dest.TravelInsuranceTotal, opt => opt.MapFrom(src => src.Details.TravelInsuranceTotal))
                .ForMember(dest => dest.ResidenceTotal, opt => opt.MapFrom(src => src.Details.ResidenceTotal))
                .ForMember(dest => dest.OfferImages, opt => opt.MapFrom(src => src.Details.OfferImages))
                .ForMember(dest => dest.OfferHotels, opt => opt.MapFrom(src => src.Details.OfferHotels))
                .ForMember(dest => dest.OfferPlanDays, opt => opt.MapFrom(src => src.Details.OfferPlanDays));


            CreateMap<eTravelAgencija.Services.Database.OfferImage, eTravelAgencija.Model.model.OfferImage>();
            CreateMap<eTravelAgencija.Services.Database.OfferHotels, eTravelAgencija.Model.model.OfferHotels>();
            CreateMap<eTravelAgencija.Services.Database.OfferPlanDay, eTravelAgencija.Model.model.OfferPlanDay>();

            CreateMap<User, Model.model.User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.LastLoginAt, opt => opt.MapFrom(src => src.LastLoginAt))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.isBlocked, opt => opt.MapFrom(src => src.isBlocked))
                .ForMember(dest => dest.Token, opt => opt.Ignore()) 
                .ForMember(dest => dest.Roles, opt => opt.Ignore()); 



            CreateMap<UserUpsertRequest, User>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
               .ForMember(dest => dest.isBlocked, opt => opt.MapFrom(_ => false));

            // 🏨 Hotel
            CreateMap<Database.Hotel, Model.model.Hotel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Stars, opt => opt.MapFrom(src => src.Stars))
                .ForMember(dest => dest.CalculatedPrice, opt => opt.MapFrom(src => src.CalculatedPrice));

            // 🛏️ HotelRooms
            CreateMap<Database.HotelRooms, Model.model.HotelRooms>()
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.RoomsLeft, opt => opt.MapFrom(src => src.RoomsLeft))
                .ForMember(dest => dest.Room, opt => opt.MapFrom(src => src.Rooms));

            // 🖼️ HotelImages
            CreateMap<Database.HotelImages, Model.model.HotelImages>()
                .ForMember(dest => dest.ImageId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.IsMain, opt => opt.MapFrom(src => src.IsMain))
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId));

            // 🏷️ Rooms
            CreateMap<Database.Rooms, Model.model.Room>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType))
                .ForMember(dest => dest.RoomCount, opt => opt.MapFrom(src => src.RoomCount));

            // 🧳 OfferHotels (ako ga koristiš u Hotel objektu)
            CreateMap<Database.OfferHotels, Model.model.OfferHotels>()
                .ForMember(dest => dest.OfferId, opt => opt.MapFrom(src => src.OfferDetailsId))
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
                .ForMember(dest => dest.DepartureDate, opt => opt.MapFrom(src => src.DepartureDate))
                .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate));

            // ✅ OfferDetails -> Offer
            CreateMap<Database.OfferDetails, Model.model.Offer>()
                .ForMember(dest => dest.OfferId, opt => opt.MapFrom(src => src.OfferId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.MinimalPrice, opt => opt.MapFrom(src => src.MinimalPrice))
                .ForMember(dest => dest.ResidenceTaxPerDay, opt => opt.MapFrom(src => src.ResidenceTaxPerDay))
                .ForMember(dest => dest.TravelInsuranceTotal, opt => opt.MapFrom(src => src.TravelInsuranceTotal))
                .ForMember(dest => dest.ResidenceTotal, opt => opt.MapFrom(src => src.ResidenceTotal))
                .ForMember(dest => dest.OfferImages, opt => opt.MapFrom(src => src.OfferImages))
                .ForMember(dest => dest.OfferPlanDays, opt => opt.MapFrom(src => src.OfferPlanDays))
                .ForMember(dest => dest.OfferHotels, opt => opt.MapFrom(src => src.OfferHotels));

               // Voucher
            CreateMap<eTravelAgencija.Services.Database.Voucher, eTravelAgencija.Model.model.Voucher>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.VoucherCode, opt => opt.MapFrom(src => src.VoucherCode))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
                .ForMember(dest => dest.priceInTokens, opt => opt.MapFrom(src => src.priceInTokens))
                .ForMember(dest => dest.UserVouchers, opt => opt.MapFrom(src => src.UserVouchers));
            
            CreateMap<eTravelAgencija.Model.model.Voucher, eTravelAgencija.Services.Database.Voucher>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.VoucherCode, opt => opt.MapFrom(src => src.VoucherCode))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
                .ForMember(dest => dest.priceInTokens, opt => opt.MapFrom(src => src.priceInTokens))
                .ForMember(dest => dest.UserVouchers, opt => opt.MapFrom(src => src.UserVouchers));
            
            
            // UserVoucher
            CreateMap<eTravelAgencija.Services.Database.UserVoucher, eTravelAgencija.Model.model.UserVoucher>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.VoucherId, opt => opt.MapFrom(src => src.VoucherId))
                .ForMember(dest => dest.User, opt => opt.Ignore()) // sprječava cikluse u JSON-u
                .ForMember(dest => dest.Voucher, opt => opt.Ignore());
            
            CreateMap<eTravelAgencija.Model.model.UserVoucher, eTravelAgencija.Services.Database.UserVoucher>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.VoucherId, opt => opt.MapFrom(src => src.VoucherId))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Voucher, opt => opt.Ignore());



        }
    }
}
