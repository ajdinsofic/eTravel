using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Services.Database
{
    public class WorkApplication
    {
        [Key]
        public int Id { get; set; }
    
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
    
        public string CvFileName { get; set; }
    
        public DateTime AppliedAt { get; set; }

        public string letter { get; set; }


    }

}