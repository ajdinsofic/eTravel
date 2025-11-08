using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eTravelAgencija.Services.Database;

public class Offer
{
    [Key] // Primary key
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public int DaysInTotal { get; set; }

    [Required]
    public string WayOfTravel { get; set; }

    // FK prema OfferSubCategory
    [ForeignKey(nameof(SubCategory))]
    public int SubCategoryId { get; set; }
    public OfferSubCategory SubCategory { get; set; }

    // Navigacija 1:1 prema OfferDetails (FK je u OfferDetails)
    public OfferDetails Details { get; set; }
}
