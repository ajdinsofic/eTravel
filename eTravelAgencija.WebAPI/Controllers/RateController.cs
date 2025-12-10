using eTravelAgencija.Model.model;
using eTravelAgencija.Model.SearchObjects;
using eTravelAgencija.Model.RequestObjects;
using eTravelAgencija.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eTravelAgencija.WebAPI.Controllers
{

    public class RateController 
        : BaseController<Rate, BaseSearchObject>
    {
        public RateController(
            ILogger<BaseController<Rate, BaseSearchObject>> logger,
            IRateService service)
            : base(logger, service)
        {
        }
    }
}
