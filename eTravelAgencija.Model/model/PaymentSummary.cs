using System;

namespace eTravelAgencija.Model.model
{
    public class PaymentSummary
    {
        // ======================
        // I RATA
        // ======================
        public bool IsFirstRateVisible { get; set; }
        public bool IsFirstRateDisabled { get; set; }
        public bool? IsFirstRatePending { get; set; }
        // null  -> ne postoji zapis
        // false -> postoji zapis, isConfirmed = false
        // true  -> postoji zapis, isConfirmed = true

        // ======================
        // II RATA
        // ======================
        public bool IsSecondRateVisible { get; set; }
        public bool IsSecondRateDisabled { get; set; }
        public bool? IsSecondRatePending { get; set; }

        // ======================
        // III RATA
        // ======================
        public bool IsThirdRateVisible { get; set; }
        public bool IsThirdRateDisabled { get; set; }
        public bool? IsThirdRatePending { get; set; }

        // ======================
        // PREOSTALI IZNOS
        // ======================
        public bool IsRemainingVisible { get; set; }
        public bool IsRemainingDisabled { get; set; }
        public bool? IsRemainingPending { get; set; }

        // ======================
        // PUNI IZNOS
        // ======================
        public bool IsFullAmountVisible { get; set; }
        public bool IsFullAmountDisabled { get; set; }
        public bool? IsFullAmountPending { get; set; }

        public DateTime? PaymentDeadline {get; set;}
    }
}
