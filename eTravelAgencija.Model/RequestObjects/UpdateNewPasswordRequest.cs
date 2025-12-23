using System.ComponentModel.DataAnnotations;

namespace eTravelAgencija.Model.Requests
{
    public class UpdateNewPasswordRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$", ErrorMessage = "Password must have at least 1 uppercase, 1 lowercase, 1 number, 1 special character, and be 6-10 characters long.")]
        public string? NewPassword { get; set; }
    }
}
