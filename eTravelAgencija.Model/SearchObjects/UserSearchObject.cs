namespace eTravelAgencija.Model.SearchObjects
{
    public class UserSearchObject : BaseSearchObject
    {
        public string? personNameSearch { get; set; }
        public bool? hasWorkAplication {get; set;}
        public bool? onlyWorkers {get; set;}
        public bool? onlyUsers {get; set;}
        public bool? onlyDirectors {get; set;}
        public bool? activeReservations { get; set; }

        public bool? CheckMoreRoles { get; set; }
    }
}