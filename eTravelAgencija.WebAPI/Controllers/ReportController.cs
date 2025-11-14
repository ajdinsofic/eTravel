using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace eTravelAgencija.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;
    
        public ReportController(IReportService service)
        {
            _service = service;
        }
    
        [HttpGet("age")]
        public async Task<IActionResult> GetAgeReport([FromQuery] string range)
        {
            return Ok(await _service.GetAgeReport(range));
        }
    
        [HttpGet("top-destinations")]
        public async Task<IActionResult> GetTopDestinations()
        {
            return Ok(await _service.GetTopDestinations());
        }
    }

}