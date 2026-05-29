using System;

namespace BlazorApp.Models.Reservations
{
    public class CreateReservationInput
    {
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime ReservationTime { get; set; } = DateTime.Now.AddDays(1);
        public int NumberOfGuests { get; set; } = 2;
        public string? Note { get; set; }
        public int? TableId { get; set; }
    }
}
