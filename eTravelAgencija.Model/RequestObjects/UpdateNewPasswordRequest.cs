using System.ComponentModel.DataAnnotations;

namespace eTravelAgencija.Model.Requests
{
    public class UpdateNewPasswordRequest
    {
        [Required]
        public int UserId { get; set; }

         [Required]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 10 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,10}$", ErrorMessage = "Password must have at least 1 uppercase, 1 lowercase, 1 number, 1 special character, and be 6-10 characters long.")]
        public string? NewPassword { get; set; }
    }
}
