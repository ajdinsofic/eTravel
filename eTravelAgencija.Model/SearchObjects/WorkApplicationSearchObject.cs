using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.SearchObjects
{
    public class WorkApplicationSearchObject : BaseSearchObject
    {
        public string? personName { get; set; }
        public int? UserId { get; set; }
    }
}