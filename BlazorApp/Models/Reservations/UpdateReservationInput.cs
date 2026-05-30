namespace BlazorApp.Models.Reservations
{
    public class UpdateReservationInput
    {
        public string Status { get; set; } = string.Empty;
        public int? TableId { get; set; }
        public string? Note { get; set; }
    }
}
