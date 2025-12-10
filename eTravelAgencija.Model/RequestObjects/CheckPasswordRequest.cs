namespace eTravelAgencija.Model.Requests
{
    public class CheckPasswordRequest
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; } = string.Empty;
    }
}
