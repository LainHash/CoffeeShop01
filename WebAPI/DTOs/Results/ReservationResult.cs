using WebAPI.DTOs.Reservations;

namespace WebAPI.DTOs.Results
{
    public class ReservationResult : ResultBase
    {
        public ReservationDTO? Reservation { get; set; }
        public List<ReservationDTO>? Reservations { get; set; }

        public ReservationResult(bool success, string message,
            ReservationDTO? reservation = null,
            List<ReservationDTO>? reservations = null) : base(success, message)
        {
            Reservation = reservation;
            Reservations = reservations;
        }
    }
}
