namespace WebAPI.DTOs.Reservations
{
    public class UpdateReservationDTO
    {
        /// <summary>Pending | Confirmed | Cancelled | Completed</summary>
        public string Status { get; set; } = null!;
        public int? TableId { get; set; }
        public string? Note { get; set; }
    }
}
