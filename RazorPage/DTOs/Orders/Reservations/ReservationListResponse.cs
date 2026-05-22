namespace RazorPage.DTOs.Orders.Reservations
{
    public class ReservationListApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<ReservationApiItem> Reservations { get; set; } = new();
    }
}
