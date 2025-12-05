using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace eTravelAgencija.Services.Database
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateBirth {get; set;}
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string MainImage {get; set;} 
        public DateTime? LastLoginAt { get; set; }
        public bool isBlocked { get; set; } = false;
        public ICollection<UserRole> UserRoles {get; set;} = new List<UserRole>();
        public ICollection<Reservation> UserReservations { get; set; } = new List<Reservation>();       

    }
}