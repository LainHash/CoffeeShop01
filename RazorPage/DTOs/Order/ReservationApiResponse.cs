using System.ComponentModel.DataAnnotations;

namespace RazorPage.DTOs.Order
{
    public class ReservationApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class ReservationInputModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string Phone { get; set; } = string.Empty;

        public string? Date { get; set; }
        public string? Time { get; set; }

        public int NumberOfGuests { get; set; } = 1;
        public string? Note { get; set; }
    }
}
