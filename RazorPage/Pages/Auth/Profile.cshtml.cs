using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Helpers.Constants.Sessions;
using System.Text.Json;
using RazorPage.DTOs.Auth;

namespace RazorPage.Pages.Auth
{
    public class ProfileModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public UserProfile ProfileData { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var roleId = HttpContext.Session.GetInt32(AccountConstants.RoleId);
            if (roleId == null)
            {
                return RedirectToPage("/Auth/Login");
            }

            var client = _httpClientFactory.CreateClient("WebAPI");

            if (roleId == 1)
            {
                var customerId = HttpContext.Session.GetString(AccountConstants.CustomerId);
                if (string.IsNullOrEmpty(customerId)) return RedirectToPage("/Auth/Login");

                var response = await client.GetAsync($"/api/Customer/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<CustomerInfoResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (result?.Success == true && result.Customer != null)
                    {
                        ProfileData = new UserProfile
                        {
                            FullName = result.Customer.FullName,
                            Phone = result.Customer.Phone,
                            Email = result.Customer.User.Email,
                            Username = result.Customer.User.Username,
                            CreatedAt = result.Customer.User.CreatedAt,
                            RoleName = "Khách hàng"
                        };
                    }
                    else
                    {
                        ErrorMessage = result?.Message ?? "Không thể lấy thông tin.";
                    }
                }
            }
            else // Manager/Employee
            {
                var managerId = HttpContext.Session.GetString("ManagerId");
                if (string.IsNullOrEmpty(managerId)) return RedirectToPage("/Auth/Login");

                var response = await client.GetAsync($"/api/Manager/{managerId}");
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ManagerInfoResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (result?.Success == true && result.Manager != null)
                    {
                        ProfileData = new UserProfile
                        {
                            FullName = result.Manager.FullName,
                            Phone = result.Manager.Phone,
                            Email = result.Manager.User.Email,
                            Username = result.Manager.User.Username,
                            CreatedAt = result.Manager.User.CreatedAt,
                            Position = result.Manager.Position,
                            RoleName = "Nhân viên / Quản lý"
                        };
                    }
                    else
                    {
                        ErrorMessage = result?.Message ?? "Không thể lấy thông tin.";
                    }
                }
            }

            return Page();
        }
    }
}
