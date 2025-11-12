using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace eTravelAgencija.Model.RequestObjects
{
    using System.ComponentModel.DataAnnotations;

public class OfferInsertRequest
{
    [Required(ErrorMessage = "Naslov ponude je obavezan.")]
    [StringLength(100, ErrorMessage = "Naslov može imati najviše 100 karaktera.")]
    public string Title { get; set; }

    [Range(1, 365, ErrorMessage = "Broj dana mora biti između 1 i 365.")]
    public int DaysInTotal { get; set; }

    [Required(ErrorMessage = "Način putovanja je obavezan.")]
    [RegularExpression("^(AVION|AUTOBUS)$", ErrorMessage = "Način putovanja može biti samo 'AVION' ili 'AUTOBUS'.")]
    public string WayOfTravel { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Podkategorija mora biti izabrana.")]
    public int SubCategoryId { get; set; } = -1;

    [Range(0, double.MaxValue, ErrorMessage = "Minimalna cijena ne može biti negativna.")]
    public decimal MinimalPrice { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Ukupna cijena osiguranja ne može biti negativna.")]
    public decimal TravelInsuranceTotal { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Ukupna cijena boravka ne može biti negativna.")]
    public decimal ResidenceTotal { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Boravišna taksa po danu ne može biti negativna.")]
    public decimal ResidenceTaxPerDay { get; set; }

    [Required(ErrorMessage = "Opis ponude je obavezan.")]
    [StringLength(2000, ErrorMessage = "Opis može imati najviše 2000 karaktera.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Država je obavezna.")]
    [StringLength(100, ErrorMessage = "Naziv države može imati najviše 100 karaktera.")]
    public string Country { get; set; }

    [Required(ErrorMessage = "Grad je obavezan.")]
    [StringLength(100, ErrorMessage = "Naziv grada može imati najviše 100 karaktera.")]
    public string City { get; set; }
}

}