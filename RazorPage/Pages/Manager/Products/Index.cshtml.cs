using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.DTOs.Manager;
using RazorPage.Helpers.Constants.Sessions;
using System.Text.Json;

namespace RazorPage.Pages.Manager.Products
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<ProductDTO> Products { get; set; } = new();
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var roleId = HttpContext.Session.GetInt32(AccountConstants.RoleId);
            if (roleId == null || roleId == 1)
            {
                return RedirectToPage("/Auth/Login");
            }

            if (TempData["SuccessMessage"] != null)
            {
                SuccessMessage = TempData["SuccessMessage"]?.ToString();
            }

            var client = _httpClientFactory.CreateClient("WebAPI");
            var response = await client.GetAsync("/api/Product");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ProductListApiResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (result?.Success == true)
                {
                    Products = result.List;
                }
                else
                {
                    ErrorMessage = result?.Message ?? "Không thể lấy danh sách sản phẩm.";
                }
            }
            else
            {
                ErrorMessage = "Dịch vụ hiện không khả dụng.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var roleId = HttpContext.Session.GetInt32(AccountConstants.RoleId);
            if (roleId == null || roleId == 1)
            {
                return RedirectToPage("/Auth/Login");
            }

            var client = _httpClientFactory.CreateClient("WebAPI");
            var response = await client.DeleteAsync($"/api/Product/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Đã xóa sản phẩm thành công.";
            }
            else
            {
                TempData["ErrorMessage"] = "Xóa sản phẩm thất bại.";
            }
            
            return RedirectToPage();
        }
    }
}
