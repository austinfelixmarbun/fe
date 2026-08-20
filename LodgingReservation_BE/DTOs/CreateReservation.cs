namespace LodgingReservation_BE.DTOs
{
    public class CreateReservation
    {
        public long UserId { get; set; }
        public long? PromotionId { get; set; }
        public long RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal LateCheckoutFee { get; set; }

        public List<ReservationAddOnItem>? AddOns { get; set; }
    }
}
