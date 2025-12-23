using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace eTravelAgencija.Model.Requests
{
    public class UserInsertRequest
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Username must be at least 6 characters long.")]
        [RegularExpression(@"^(?=.*\d).{6,}$", ErrorMessage = "Username must contain at least one number.")]
        public string Username { get; set; } = string.Empty;

        public DateTime DateBirth { get; set; }

        [Required]
        [RegularExpression(@"^\+[1-9]\d{1,14}$",
        ErrorMessage = "Phone number must be in international E.164 format, e.g. +38761234567")]
        public string? PhoneNumber { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]   
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$", ErrorMessage = "Password must have at least 1 uppercase, 1 lowercase, 1 number, 1 special character, and be 6-10 characters long.")]
        public string? Password { get; set; }
        public int RoleId { get; set; }
    }
}