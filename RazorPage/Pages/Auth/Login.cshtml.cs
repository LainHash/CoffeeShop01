using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace RazorPage.Pages.Auth
{
    public class LoginInputModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }

    // Ánh xạ response trả về từ WebAPI
    internal class LoginApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public CustomerInfo? Customer { get; set; }
    }

    internal class CustomerInfo
    {
        public Guid PublicId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }

    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<LoginModel> _logger;

        [BindProperty]
        public LoginInputModel Input { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public LoginModel(IHttpClientFactory httpClientFactory, ILogger<LoginModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            // Redirect nếu đã đăng nhập
            if (HttpContext.Session.GetString("CustomerEmail") != null)
                return RedirectToPage("/Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Vui lòng kiểm tra lại thông tin.";
                return Page();
            }

            try
            {
                var client = _httpClientFactory.CreateClient("WebAPI");

                var payload = new { email = Input.Email, password = Input.Password };
                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/Customer/Login", content);
                var responseBody = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<LoginApiResponse>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (result?.Success == true && result.Customer != null)
                {
                    // Lưu thông tin user vào Session của RazorPage
                    HttpContext.Session.SetString("CustomerId", result.Customer.PublicId.ToString());
                    HttpContext.Session.SetString("CustomerEmail", result.Customer.Email);
                    HttpContext.Session.SetString("CustomerUsername", result.Customer.Username);
                    HttpContext.Session.SetString("CustomerFullName", result.Customer.FullName);
                    HttpContext.Session.SetString("CustomerPhone", result.Customer.Phone);

                    return RedirectToPage("/Index");
                }

                ErrorMessage = result?.Message ?? "Đăng nhập thất bại. Vui lòng thử lại.";
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login request failed");
                ErrorMessage = "Dịch vụ tạm thời không khả dụng. Vui lòng thử lại sau.";
                return Page();
            }
        }
    }
}
