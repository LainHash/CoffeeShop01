using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Helpers.Constants.Sessions;
using System.Text;
using System.Text.Json;
using RazorPage.DTOs.Auth;
using RazorPage.DTOs.Accounts;

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
            if (HttpContext.Session.GetString(AccountConstants.Email) != null)
            {
                return RedirectToPage("/Home/Index");
            }
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

                var response = await client.PostAsync("/api/Auth/Login", content);
                var responseBody = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<AuthApiResponse>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (response.IsSuccessStatusCode && result?.Success == true)
                {
                    HttpContext.Session.SetInt32(AccountConstants.RoleId, result.RoleId);

                    var manager = result.Data;
                    HttpContext.Session.SetString(AccountConstants.ManagerId, manager!.PublicId.ToString());
                    HttpContext.Session.SetString(AccountConstants.Email, manager.Email);
                    HttpContext.Session.SetString(AccountConstants.Username, manager.Username);
                    HttpContext.Session.SetString(AccountConstants.FullName, manager.FullName);
                    HttpContext.Session.SetString(AccountConstants.Phone, manager.Phone);
                    HttpContext.Session.SetString("Position", manager.Position ?? "");

                    return RedirectToPage("/Employee/POS");
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
