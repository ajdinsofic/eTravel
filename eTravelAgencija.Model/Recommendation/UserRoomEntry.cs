using Microsoft.ML.Data;

namespace eTravelAgencija.Services.Recommendations.MLModels
{

    public class UserRoomEntry
    {
        [LoadColumn(0)]
        public uint UserId { get; set; }

        [LoadColumn(1)]
        public uint RoomId { get; set; }

        [LoadColumn(2)]
        public float Label { get; set; } = 1;
    }
}
