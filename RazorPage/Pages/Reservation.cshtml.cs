using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace RazorPage.Pages
{
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

    public class ReservationModel : PageModel
    {
        [BindProperty]
        public ReservationInputModel Input { get; set; } = new();

        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
            // Pre-fill nếu đã đăng nhập
            var email = HttpContext.Session.GetString("CustomerEmail");
            var fullName = HttpContext.Session.GetString("CustomerFullName");
            var phone = HttpContext.Session.GetString("CustomerPhone");

            if (email != null) Input.Email = email;
            if (fullName != null) Input.FullName = fullName;
            if (phone != null) Input.Phone = phone;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Vui lòng điền đầy đủ các thông tin bắt buộc.";
                return Page();
            }

            // In a real scenario, you'd call the API here to save the reservation.
            // For now, we simulate a successful booking.
            SuccessMessage = $"Cảm ơn bạn, {Input.FullName}! Bàn cho {Input.NumberOfGuests} người đã được đặt thành công. Chúng tôi sẽ gửi email xác nhận đến {Input.Email}.";

            // Clear form after success
            Input = new ReservationInputModel();
            ModelState.Clear();

            return Page();
        }
    }
}
