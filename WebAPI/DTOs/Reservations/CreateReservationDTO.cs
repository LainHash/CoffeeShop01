namespace WebAPI.DTOs.Reservations
{
    public class CreateReservationDTO
    {
        public Guid CustomerPublicId { get; set; }
        public DateTime ReservationTime { get; set; }
        public int NumberOfGuests { get; set; }
        public string? Note { get; set; }
    }
}
