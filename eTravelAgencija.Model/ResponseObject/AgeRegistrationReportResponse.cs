using System.Collections.Generic;

namespace eTravelAgencija.Model.ResponseObject
{
   public class AgeRegistrationReportResponse
   {
       public int TotalRegistrations { get; set; }
       public List<AgeGroupStatsResponse> AgeGroups { get; set; }
   }    
}


