using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using eTravelAgencija.Services.Database;
using Microsoft.AspNetCore.Identity;

public class UserRole : IdentityUserRole<int>
{
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

}

