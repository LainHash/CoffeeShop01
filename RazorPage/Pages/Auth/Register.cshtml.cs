using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace RazorPage.Pages.Auth
{
    public class RegisterInputModel
    {
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
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
                ErrorMessage = "Please fix the errors below.";
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
                    TempData["SuccessMessage"] = "Account created successfully! Please log in.";
                    return RedirectToPage("/Auth/Login");
                }
                else
                {
                    ErrorMessage = result?.Message ?? "Registration failed. Please try again.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration failed");
                ErrorMessage = "Service unavailable. Please try again later.";
                return Page();
            }
        }
    }
}
