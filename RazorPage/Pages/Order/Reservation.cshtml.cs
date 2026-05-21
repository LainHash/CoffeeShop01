using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Helpers.Constants.Sessions;
using System.ComponentModel.DataAnnotations;
using RazorPage.DTOs.Order;

namespace RazorPage.Pages.Order
{
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

        /// <summary>
        /// Exposed for the Razor view — true when the current user is staff/manager (roleId != 1).
        /// </summary>
        public bool IsStaff { get; private set; }

        private bool CheckIsStaff()
        {
            var roleId = HttpContext.Session.GetInt32(AccountConstants.RoleId);
            return roleId.HasValue && roleId.Value != 1;
        }

        public IActionResult OnGet()
        {
            // All roles (and guests) can visit the page.
            // Staff will see the form; customers/guests will see the contact info view.
            IsStaff = CheckIsStaff();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            IsStaff = CheckIsStaff();

            // Only staff members are allowed to submit the reservation form.
            if (!IsStaff)
            {
                return Forbid();
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
                ErrorMessage = "Ngày giờ không hợp lệ. Vui lòng kiểm tra lại.";
                return Page();
            }

            // Build payload – staff creates reservations on behalf of customers;
            // we pass customer info from the form rather than from session.
            var payload = new
            {
                FullName = Input.FullName,
                Email = Input.Email,
                Phone = Input.Phone,
                ReservationTime = reservationTime,
                NumberOfGuests = Input.NumberOfGuests,
                Note = Input.Note
            };

            var client = _httpClientFactory.CreateClient("WebAPI");
            var json = System.Text.Json.JsonSerializer.Serialize(payload);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/Reservation", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            var result = System.Text.Json.JsonSerializer.Deserialize<ReservationApiResponse>(
                responseBody,
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (response.IsSuccessStatusCode && result?.Success == true)
            {
                SuccessMessage = $"Đặt bàn thành công! Bàn cho {Input.NumberOfGuests} người vào lúc {reservationTime:dd/MM/yyyy HH:mm} đã được ghi nhận cho khách hàng {Input.FullName}.";
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
}
