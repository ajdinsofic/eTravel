namespace eTravelAgencija.Model.SearchObjects
{
    public class UserSearchObject : BaseSearchObject
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public bool? hasWorkAplication {get; set;}
        public bool? onlyWorkers {get; set;}
    }
}