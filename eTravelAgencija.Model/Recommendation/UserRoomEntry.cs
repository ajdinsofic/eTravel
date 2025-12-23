using Microsoft.ML.Data;

namespace eTravelAgencija.Services.Recommendations.MLModels
{

    public class UserRoomEntry
    {
        [KeyType]
        public uint UserId { get; set; }

        [KeyType]
        public uint RoomId { get; set; }

        // One-class implicit feedback
        public float Label { get; set; } = 1f;
    }
}
