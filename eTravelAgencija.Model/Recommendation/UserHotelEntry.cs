using Microsoft.ML.Data;

namespace eTravelAgencija.Services.Recommendations.MLModels
{

    public class UserHotelEntry
    {
        [KeyType]
        public uint UserId { get; set; }

        [KeyType]
        public uint HotelId { get; set; }

        // One-class implicit feedback
        public float Label { get; set; } = 1f;
    }
}
