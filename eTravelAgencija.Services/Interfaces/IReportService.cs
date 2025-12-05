using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTravelAgencija.Model.ResponseObject;

namespace eTravelAgencija.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<AgeGroupStatsResponse>> GetAgeReport(string range);
        Task<List<DestinationStatsResponse>> GetTopDestinations();
    }
}