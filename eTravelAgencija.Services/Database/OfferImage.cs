using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eTravelAgencija.Services.Database;

public class OfferImage
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(OfferDetails))]
    public int OfferId { get; set; }  // FK prema OfferDetails

    public OfferDetails OfferDetails { get; set; }

    [Required]
    public string ImageUrl { get; set; }
}
