namespace BlazorApp.Models.Reservations
{
    public class ReservationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public ReservationModel? Data { get; set; }
    }
}
