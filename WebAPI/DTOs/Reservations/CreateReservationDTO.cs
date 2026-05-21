namespace WebAPI.DTOs.Reservations
{
    public class CreateReservationDTO
    {
        /// <summary>Họ tên khách hàng (nhân viên nhập trực tiếp)</summary>
        public string FullName { get; set; } = null!;

        /// <summary>Số điện thoại khách hàng — dùng để upsert Customer</summary>
        public string Phone { get; set; } = null!;

        public DateTime ReservationTime { get; set; }
        public int NumberOfGuests { get; set; }
        public string? Note { get; set; }
    }
}
