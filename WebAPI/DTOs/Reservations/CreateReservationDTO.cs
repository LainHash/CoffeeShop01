namespace WebAPI.DTOs.Reservations
{
    public class CreateReservationDTO
    {
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime ReservationTime { get; set; }
        public int NumberOfGuests { get; set; }
        public string? Note { get; set; }
        public int? TableId { get; set; }
    }
}
