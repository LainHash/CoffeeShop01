using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace RazorPage.Pages
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    public class MenuModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<MenuModel> _logger;

        public List<ProductDto> Products { get; set; } = new();
        public List<CategoryDto> Categories { get; set; } = new();
        public string? ApiError { get; set; }

        public MenuModel(IHttpClientFactory httpClientFactory, ILogger<MenuModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("WebAPI");

            // Fetch products
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
                    ApiError = "Could not load products. Please try again later.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to fetch products");
                ApiError = "API service unavailable. Showing placeholder content.";
            }

            // Fetch categories
            try
            {
                var catResponse = await client.GetAsync("/api/Category");
                if (catResponse.IsSuccessStatusCode)
                {
                    var json = await catResponse.Content.ReadAsStringAsync();
                    var categories = JsonSerializer.Deserialize<List<CategoryDto>>(json, new JsonSerializerOptions
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
