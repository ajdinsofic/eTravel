using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace eTravelAgencija.Model.RequestObjects
{
    public class UserRoleUpsertRequest
    {
        [Required]
        public int userId { get; set; }

        [Required]
        public int roleId { get; set; }
    }
}
