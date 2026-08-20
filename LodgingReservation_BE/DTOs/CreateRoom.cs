using LodgingReservation_BE.Models.Enum;

namespace LodgingReservation_BE.DTOs
{
    public class CreateRoom
    {
        public long RoomTypeId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public RoomStatus Status { get; set; }
    }
}
