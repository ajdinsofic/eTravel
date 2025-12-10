using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTravelAgencija.Model.model
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime DateBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string Token { get; set; }
        public bool isBlocked { get; set; }
        public string ImageUrl {get; set;} 

        
    }
}