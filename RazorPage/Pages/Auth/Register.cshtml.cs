using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace RazorPage.Pages.Auth
{
    public class RegisterInputModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    internal class RegisterApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }

    public class RegisterModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<RegisterModel> _logger;

        [BindProperty]
        public RegisterInputModel Input { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public RegisterModel(IHttpClientFactory httpClientFactory, ILogger<RegisterModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("CustomerEmail") != null)
                return RedirectToPage("/Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Vui lòng sửa các lỗi bên dưới.";
                return Page();
            }

            try
            {
                var client = _httpClientFactory.CreateClient("WebAPI");
                var payload = new
                {
                    email = Input.Email,
                    phone = Input.Phone,
                    username = Input.Username,
                    password = Input.Password,
                    confirmPassword = Input.ConfirmPassword,
                    fullName = Input.FullName
                };

                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/Customer/Register", content);
                var responseBody = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<RegisterApiResponse>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result?.Success == true)
                {
                    TempData["SuccessMessage"] = "Tài khoản đã được tạo thành công! Vui lòng đăng nhập.";
                    return RedirectToPage("/Auth/Login");
                }
                else
                {
                    ErrorMessage = result?.Message ?? "Đăng ký thất bại. Vui lòng thử lại.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration failed");
                ErrorMessage = "Dịch vụ không khả dụng. Vui lòng thử lại sau.";
                return Page();
            }
        }
    }
}
