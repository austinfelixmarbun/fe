namespace LodgingReservation_BE.DTOs
{
    public class ReservationAddOnItem
    {
        public long ExtraServiceId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
