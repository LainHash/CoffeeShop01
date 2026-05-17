using WebAPI.DTOs.Reservations;

namespace WebAPI.DTOs.Results
{
    public class ReservationResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public ReservationDTO? Reservation { get; set; }
        public List<ReservationDTO>? Reservations { get; set; }

        public ReservationResult(bool success, string message,
            ReservationDTO? reservation = null,
            List<ReservationDTO>? reservations = null)
        {
            Success = success;
            Message = message;
            Reservation = reservation;
            Reservations = reservations;
        }
    }
}
