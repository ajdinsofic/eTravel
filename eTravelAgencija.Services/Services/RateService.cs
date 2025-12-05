using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Services.Interfaces;

namespace eTravelAgencija.Services.Services
{
    public class RateService : BaseService<Model.model.Rate, BaseSearchObject, Database.Rate>, IRateService    
    {
        public RateService(eTravelAgencijaDbContext context, AutoMapper.IMapper mapper) : base(context, mapper)
        {
        }
    }
}