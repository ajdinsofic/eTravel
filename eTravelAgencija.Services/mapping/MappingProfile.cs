using AutoMapper;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Model.RequestObjects;
using System.Linq;
using eTravelAgencija.Model.Requests;
using System;
using eTravelAgencija.Model.ResponseObject;

namespace eTravelAgencija.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Pocetak Offer i Offerdetails
            CreateMap<OfferInsertRequest, Database.Offer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.Details, opt => opt.Ignore()) 
                .ForMember(dest => dest.SubCategory, opt => opt.Ignore()); 

            
            CreateMap<OfferUpdateRequest, Database.Offer>()
              .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
              .ForMember(dest => dest.DaysInTotal, opt => opt.MapFrom(src => src.DaysInTotal))
              .ForMember(dest => dest.WayOfTravel, opt => opt.MapFrom(src => src.WayOfTravel))
              .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategoryId))
              .ForMember(dest => dest.Details, opt => opt.Ignore())
              .ForMember(dest => dest.SubCategory, opt => opt.Ignore());

            CreateMap<Database.Offer, OfferUpdateRequest>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.DaysInTotal, opt => opt.MapFrom(src => src.DaysInTotal))
                .ForMember(dest => dest.WayOfTravel, opt => opt.MapFrom(src => src.WayOfTravel))
                .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategoryId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Details.Description))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Details.Country))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Details.City))
                .ForMember(dest => dest.MinimalPrice, opt => opt.MapFrom(src => src.Details.MinimalPrice))
                .ForMember(dest => dest.TravelInsuranceTotal, opt => opt.MapFrom(src => src.Details.TravelInsuranceTotal))
                .ForMember(dest => dest.ResidenceTotal, opt => opt.MapFrom(src => src.Details.ResidenceTotal))
                .ForMember(dest => dest.ResidenceTaxPerDay, opt => opt.MapFrom(src => src.Details.ResidenceTaxPerDay));

            CreateMap<Database.Offer, Model.model.Offer>()
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

            // Kraj Offer i OfferDetails

            CreateMap<eTravelAgencija.Services.Database.OfferHotels, eTravelAgencija.Model.model.OfferHotels>();   


            // Pocetak User
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

            CreateMap<UserUpsertRequest, Database.User>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
               .ForMember(dest => dest.isBlocked, opt => opt.MapFrom(_ => false));

            CreateMap<User, UserLoginResponse>()
                .ForMember(dest => dest.Userid, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.Roles, opt => opt.Ignore());


            // Kraj User

            // 🏨 Hotel
            CreateMap<Database.Hotel, Model.model.Hotel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Stars, opt => opt.MapFrom(src => src.Stars))
                .ForMember(dest => dest.CalculatedPrice, opt => opt.MapFrom(src => src.CalculatedPrice));

            CreateMap<HotelUpsertRequest, eTravelAgencija.Services.Database.Hotel>()
                .ForMember(dest => dest.HotelRooms, opt => opt.Ignore())
                .ForMember(dest => dest.HotelImages, opt => opt.Ignore())
                .ForMember(dest => dest.OfferHotels, opt => opt.Ignore())
                .ForMember(dest => dest.CalculatedPrice, opt => opt.Ignore()); 

            // HotelRooms
            CreateMap<HotelRoomInsertRequest, Database.HotelRooms>()
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.RoomsLeft, opt => opt.MapFrom(src => src.RoomsLeft))
                .ForMember(dest => dest.Hotel, opt => opt.Ignore())
                .ForMember(dest => dest.Rooms, opt => opt.Ignore());
            
            CreateMap<HotelRoomUpdateRequest, Database.HotelRooms>()
                .ForMember(dest => dest.RoomsLeft, opt => opt.MapFrom(src => src.RoomsLeft))
                .ForMember(dest => dest.Hotel, opt => opt.Ignore())
                .ForMember(dest => dest.Rooms, opt => opt.Ignore());
            
            CreateMap<eTravelAgencija.Services.Database.HotelRooms,
                      eTravelAgencija.Model.model.HotelRooms>()
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.RoomsLeft, opt => opt.MapFrom(src => src.RoomsLeft))
                .ForMember(dest => dest.Hotel, opt => opt.MapFrom(src => src.Hotel))
                .ForMember(dest => dest.Room, opt => opt.MapFrom(src => src.Rooms));
            

            // 🖼️ HotelImages
            CreateMap<Database.HotelImages, Model.model.HotelImages>()
                .ForMember(dest => dest.ImageId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.IsMain, opt => opt.MapFrom(src => src.IsMain))
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId));

            
            CreateMap<HotelImageUpsertRequest, eTravelAgencija.Services.Database.HotelImages>()
                .ForMember(dest => dest.Hotel, opt => opt.Ignore());

            // OfferImages
            CreateMap<OfferImageUpsertRequest, Database.OfferImage>()
               .ForMember(dest => dest.isMain, opt => opt.MapFrom(src => src.IsMain))
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.OfferDetails, opt => opt.Ignore());

            CreateMap<eTravelAgencija.Services.Database.OfferImage, eTravelAgencija.Model.model.OfferImage>();


            //OfferPlanDay
            CreateMap<OfferPlanDayUpdateRequest, Database.OfferPlanDay>()
                 .ForMember(dest => dest.DayTitle, opt => opt.MapFrom(src => src.Title))
                 .ForMember(dest => dest.DayDescription, opt => opt.MapFrom(src => src.Description))
                 .ForMember(dest => dest.OfferDetails, opt => opt.Ignore()); // obavezno ignorisati


            CreateMap<OfferPlanDayInsertRequest, Database.OfferPlanDay>()
                .ForMember(dest => dest.DayTitle, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.DayDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.OfferDetails, opt => opt.Ignore()); // uvijek ignorisi navigation

            CreateMap<eTravelAgencija.Services.Database.OfferPlanDay, eTravelAgencija.Model.model.OfferPlanDay>();


            // 🏷️ Rooms
            CreateMap<Database.Rooms, Model.model.Room>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType))
                .ForMember(dest => dest.RoomCount, opt => opt.MapFrom(src => src.RoomCount));

            // 🧳 OfferHotels (ako ga koristiš u Hotel objektu)
            CreateMap<OfferHotelInsertRequest, Database.OfferHotels>()
                .ForMember(dest => dest.OfferDetailsId, opt => opt.MapFrom(src => src.OfferId))
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
                .ForMember(dest => dest.DepartureDate, opt => opt.MapFrom(src => src.DepartureDate ?? DateTime.MinValue))
                .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate ?? DateTime.MinValue))
                .ForMember(dest => dest.OfferDetails, opt => opt.Ignore())
                .ForMember(dest => dest.Hotel, opt => opt.Ignore());
            
            CreateMap<OfferHotelUpdateRequest, Database.OfferHotels>()
                .ForMember(dest => dest.DepartureDate, opt => opt.MapFrom(src => src.DepartureDate ?? default))
                .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate ?? default))
                .ForMember(dest => dest.OfferDetails, opt => opt.Ignore())
                .ForMember(dest => dest.Hotel, opt => opt.Ignore());
            
            CreateMap<eTravelAgencija.Services.Database.OfferHotels,
                      eTravelAgencija.Model.model.OfferHotels>()
                .ForMember(dest => dest.OfferId, opt => opt.MapFrom(src => src.OfferDetailsId))
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
                .ForMember(dest => dest.DepartureDate, opt => opt.MapFrom(src => src.DepartureDate))
                .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate));


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

            CreateMap<VoucherUpsertRequest, Database.Voucher>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.VoucherCode, opt => opt.MapFrom(src => src.VoucherCode))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
                .ForMember(dest => dest.priceInTokens, opt => opt.MapFrom(src => src.priceInTokens))
                .ForMember(dest => dest.UserVouchers, opt => opt.Ignore());

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

            //

            CreateMap<ReservationPreviewRequest, Model.model.ReservationPreview>()
                .ForMember(dest => dest.OfferTitle, opt => opt.Ignore())
                .ForMember(dest => dest.HotelTitle, opt => opt.Ignore())
                .ForMember(dest => dest.HotelStars, opt => opt.Ignore())
                .ForMember(dest => dest.RoomType, opt => opt.Ignore())
                .ForMember(dest => dest.Insurance, opt => opt.Ignore())
                .ForMember(dest => dest.ResidenceTaxTotal, opt => opt.Ignore())
                .ForMember(dest => dest.TotalPrice, opt => opt.Ignore());



            // Reservation
            CreateMap<ReservationUpsertRequest, Database.Reservation>()
                .ForMember(dest => dest.addedNeeds,
                           opt => opt.MapFrom(src => src.AddedNeeds))
                .ForMember(dest => dest.Payments, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ReservationUpsertRequest, eTravelAgencija.Model.model.Reservation>()
                .ForMember(dest => dest.UserNeeds,
                           opt => opt.MapFrom(src => src.AddedNeeds))
                .ForMember(dest => dest.Payments, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Database.Reservation,Model.model.Reservation>()
                .ForMember(dest => dest.UserNeeds,
                           opt => opt.MapFrom(src => src.addedNeeds))
                .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments));


            // Payment
            CreateMap<PaymentInsertRequest, Database.Payment>()        
                .ForMember(dest => dest.PaymentDeadline, opt => opt.Ignore()); 

            CreateMap<Database.Payment, Model.model.Payment>()
                .ForMember(dest => dest.reservation, opt => opt.MapFrom(src => src.Reservation))
                .ForMember(dest => dest.rate, opt => opt.MapFrom(src => src.Rate));
            
                CreateMap<PaymentUpdateRequest, eTravelAgencija.Services.Database.Payment>()
                .ForMember(dest => dest.Reservation, opt => opt.Ignore())
                .ForMember(dest => dest.Rate, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentDeadline, opt => opt.Ignore()); 

            // Comments
            CreateMap<CommentUpsertRequest, eTravelAgencija.Services.Database.Comment>()
                .ForMember(dest => dest.user, opt => opt.Ignore())   
                .ForMember(dest => dest.offer, opt => opt.Ignore()); 

            CreateMap<eTravelAgencija.Services.Database.Comment, eTravelAgencija.Model.model.Comment>()
                .ForMember(dest => dest.user, opt => opt.MapFrom(src => src.user))
                .ForMember(dest => dest.offer, opt => opt.MapFrom(src => src.offer));

            // WorkApplication
            CreateMap<WorkApplicationUpsertRequest, WorkApplication>()
               .ForMember(dest => dest.CvFileName, opt => opt.Ignore()) 
               .ForMember(dest => dest.AppliedAt, opt => opt.Ignore())
               .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<eTravelAgencija.Services.Database.WorkApplication, eTravelAgencija.Model.model.WorkApplication>()
               .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<eTravelAgencija.Services.Database.WorkApplication, CVDownloadResponse>()
                .ForMember(dest => dest.fileBytes, opt => opt.Ignore());

        }
    }
}
