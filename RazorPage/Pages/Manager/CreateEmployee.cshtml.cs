using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Helpers.Constants.Sessions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text;
using RazorPage.DTOs.Manager;

namespace RazorPage.Pages.Manager
{
    public class CreateEmployeeModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateEmployeeModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public CreateEmployeeInput Input { get; set; } = new();

        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            // Check if manager is logged in (RoleId != 1)
            var roleId = HttpContext.Session.GetInt32(AccountConstants.RoleId);
            if (roleId == null || roleId == 1)
            {
                return RedirectToPage("/Auth/Login");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var roleId = HttpContext.Session.GetInt32(AccountConstants.RoleId);
            if (roleId == null || roleId == 1)
            {
                return RedirectToPage("/Auth/Login");
            }

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Vui lòng nhập đầy đủ và hợp lệ thông tin.";
                return Page();
            }

            if (Input.Password != Input.ConfirmPassword)
            {
                ErrorMessage = "Mật khẩu xác nhận không khớp.";
                return Page();
            }

            var client = _httpClientFactory.CreateClient("WebAPI");
            var json = JsonSerializer.Serialize(Input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/Manager/Create/Employee", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<CreateEmployeeApiResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (response.IsSuccessStatusCode && result?.Success == true)
            {
                SuccessMessage = "Tạo tài khoản nhân viên thành công!";
                Input = new CreateEmployeeInput();
                ModelState.Clear();
            }
            else
            {
                ErrorMessage = result?.Message ?? "Có lỗi xảy ra khi tạo nhân viên.";
            }

            return Page();
        }
    }
}
