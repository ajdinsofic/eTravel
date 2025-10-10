using System;
using System.Collections.Generic;

namespace eTravelAgencija.Model.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public string? PhoneNumber { get; set; }
        public bool isBlocked { get; set; }
        public List<RoleResponse> Roles { get; set; } = new List<RoleResponse>();
    }
} 