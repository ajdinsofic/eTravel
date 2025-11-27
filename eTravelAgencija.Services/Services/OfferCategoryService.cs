using AutoMapper;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;
using eTravelAgencija.Services.Services;

namespace eTravelAgencija.Services.Services
{
    public class OfferCategoryService : BaseService<Model.model.OfferCategory, BaseSearchObject, Database.OfferCategory>, IOfferCategoryService
    {
        public OfferCategoryService(eTravelAgencijaDbContext context, IMapper mapper):base(context, mapper)
        {
            
        }
    }
}