using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.DTOs.Categories;
using RazorPage.DTOs.Products;
using System.Text.Json;

namespace RazorPage.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IndexModel> _logger;

        public List<ProductDTO> Products { get; set; } = new();
        public List<CategoryDTO> Categories { get; set; } = new();
        public string? ApiError { get; set; }

        public IndexModel(IHttpClientFactory httpClientFactory, ILogger<IndexModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("WebAPI");

            try
            {
                var productResponse = await client.GetAsync("/api/Product");
                if (productResponse.IsSuccessStatusCode)
                {
                    var json = await productResponse.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ProductListResponse>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    if (result?.Success == true && result.List != null)
                    {
                        Products = result.List;
                    }
                }
                else
                {
                    ApiError = "Không thể tải danh sách sản phẩm. Vui lòng thử lại sau.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to fetch products");
                ApiError = "Dịch vụ API hiện không khả dụng. Vui lòng quay lại sau.";
            }

            // Fetch categories
            try
            {
                var catResponse = await client.GetAsync("/api/Category");
                if (catResponse.IsSuccessStatusCode)
                {
                    var json = await catResponse.Content.ReadAsStringAsync();
                    var categories = JsonSerializer.Deserialize<List<CategoryDTO>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    if (categories != null)
                        Categories = categories;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to fetch categories");
            }
        }
    }
}
