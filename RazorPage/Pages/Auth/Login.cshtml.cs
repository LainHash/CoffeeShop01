using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.DTOs.Accounts.Customers;
using RazorPage.Helpers.Constants.Sessions;
using System.Text;
using System.Text.Json;

namespace RazorPage.Pages.Auth
{

    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<LoginModel> _logger;

        [BindProperty]
        public LoginDTO Input { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public LoginModel(IHttpClientFactory httpClientFactory, ILogger<LoginModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            // Redirect nếu đã đăng nhập
            if (HttpContext.Session.GetString(AccountConstants.Email) != null)
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
                    var customer = result.Customer;
                    HttpContext.Session
                        .SetString(AccountConstants.CustomerId, customer.PublicId.ToString());
                    HttpContext.Session
                        .SetString(AccountConstants.Email, customer.User.Email);
                    HttpContext.Session
                        .SetString(AccountConstants.Username, customer.User.Username);
                    HttpContext.Session
                        .SetString(AccountConstants.FullName, customer.FullName);
                    HttpContext.Session
                        .SetString(AccountConstants.Phone, customer.Phone);

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
