using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace RazorPage.Pages
{
    public class ReservationInputModel
    {
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
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
                ErrorMessage = "Please fill in all required fields correctly.";
                return Page();
            }

            // In a real scenario, you'd call the API here to save the reservation.
            // For now, we simulate a successful booking.
            SuccessMessage = $"Thank you, {Input.FullName}! Your table for {Input.NumberOfGuests} person(s) has been reserved. We will confirm by email at {Input.Email}.";

            // Clear form after success
            Input = new ReservationInputModel();
            ModelState.Clear();

            return Page();
        }
    }
}
