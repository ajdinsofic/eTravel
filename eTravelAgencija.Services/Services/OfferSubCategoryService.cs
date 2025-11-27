using System.Linq;
using AutoMapper;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Database;
using eTravelAgencija.Services.Interfaces;
using eTravelAgencija.Services.Services;

namespace eTravelAgencija.Services.Services
{
    public class OfferSubCategoryService : BaseService<Model.model.OfferSubCategory, OfferSubCategorySearchObject, Database.OfferSubCategory>, IOfferSubCategoryService
    {
        public OfferSubCategoryService(eTravelAgencijaDbContext context, IMapper mapper):base(context, mapper)
        {
            
        }

        public override IQueryable<OfferSubCategory> ApplyFilter(IQueryable<OfferSubCategory> query, OfferSubCategorySearchObject search)
        {
            if (search.categoryId.HasValue)
            {
                query = query.Where(s => s.CategoryId == search.categoryId);
            }
            return base.ApplyFilter(query, search);
        }
    }
}