using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.model
{
    public partial class WorkApplication
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string CvFileName { get; set; }
        public DateTime AppliedAt { get; set; }
    }

}