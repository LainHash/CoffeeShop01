using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPage.DTOs.Manager;
using RazorPage.Helpers.Constants.Sessions;
using System.Text;
using System.Text.Json;

namespace RazorPage.Pages.Manager.Products
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public CreateProductDTO Input { get; set; } = new();

        public List<SelectListItem> Categories { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var roleId = HttpContext.Session.GetInt32(AccountConstants.RoleId);
            if (roleId == null || roleId == 1)
            {
                return RedirectToPage("/Auth/Login");
            }

            await LoadCategoriesAsync();
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
                await LoadCategoriesAsync();
                ErrorMessage = "Vui lòng kiểm tra lại thông tin.";
                return Page();
            }

            var client = _httpClientFactory.CreateClient("WebAPI");
            var json = JsonSerializer.Serialize(Input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/Product", content);
            
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Đã thêm sản phẩm mới thành công!";
                return RedirectToPage("Index");
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ProductApiResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ErrorMessage = result?.Message ?? "Thêm sản phẩm thất bại.";
                await LoadCategoriesAsync();
                return Page();
            }
        }

        private async Task LoadCategoriesAsync()
        {
            var client = _httpClientFactory.CreateClient("WebAPI");
            var response = await client.GetAsync("/api/Category");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<CategoryListApiResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (result?.Success == true && result.List != null)
                {
                    Categories = result.List.Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.CategoryName
                    }).ToList();
                }
            }
        }
    }
}
