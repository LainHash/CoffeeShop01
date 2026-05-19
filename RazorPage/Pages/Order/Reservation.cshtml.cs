using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Helpers.Constants.Sessions;
using System.ComponentModel.DataAnnotations;

namespace RazorPage.Pages.Order
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
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public ReservationInputModel Input { get; set; } = new();

        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            var customerId = HttpContext.Session.GetString(AccountConstants.CustomerId);
            if (string.IsNullOrEmpty(customerId))
            {
                return RedirectToPage("/Auth/Login");
            }

            var email = HttpContext.Session.GetString(AccountConstants.Email);
            var fullName = HttpContext.Session.GetString(AccountConstants.FullName);
            var phone = HttpContext.Session.GetString(AccountConstants.Phone);

            if (email != null) Input.Email = email;
            if (fullName != null) Input.FullName = fullName;
            if (phone != null) Input.Phone = phone;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var customerIdStr = HttpContext.Session.GetString(AccountConstants.CustomerId);
            if (string.IsNullOrEmpty(customerIdStr))
            {
                return RedirectToPage("/Auth/Login");
            }

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Vui lòng điền đầy đủ các thông tin bắt buộc.";
                return Page();
            }

            if (string.IsNullOrEmpty(Input.Date) || string.IsNullOrEmpty(Input.Time))
            {
                ErrorMessage = "Vui lòng chọn ngày và giờ đặt bàn.";
                return Page();
            }

            if (!DateTime.TryParse($"{Input.Date} {Input.Time}", out DateTime reservationTime))
            {
                ErrorMessage = "Ngày giờ không hợp lệ.";
                return Page();
            }

            var payload = new
            {
                CustomerPublicId = Guid.Parse(customerIdStr),
                ReservationTime = reservationTime,
                NumberOfGuests = Input.NumberOfGuests,
                Note = Input.Note
            };

            var client = _httpClientFactory.CreateClient("WebAPI");
            var json = System.Text.Json.JsonSerializer.Serialize(payload);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/Reservation", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            var result = System.Text.Json.JsonSerializer.Deserialize<ReservationApiResponse>(responseBody, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (response.IsSuccessStatusCode && result?.Success == true)
            {
                SuccessMessage = $"Cảm ơn bạn, {Input.FullName}! Bàn cho {Input.NumberOfGuests} người đã được đặt thành công. Chúng tôi sẽ gửi email xác nhận đến {Input.Email}.";
                Input = new ReservationInputModel();
                ModelState.Clear();
            }
            else
            {
                ErrorMessage = result?.Message ?? "Có lỗi xảy ra khi đặt bàn. Vui lòng thử lại.";
            }

            return Page();
        }
    }

    public class ReservationApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
