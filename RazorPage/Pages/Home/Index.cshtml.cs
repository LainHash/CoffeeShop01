using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.DTOs.Products;
using System.Text.Json;

namespace RazorPage.Pages.Home
{

    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IndexModel> _logger;

        public List<ProductDTO> Products { get; set; } = new();
        public string? ApiError { get; set; }

        public IndexModel(IHttpClientFactory httpClientFactory, ILogger<IndexModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("WebAPI");
                var response = await client.GetAsync("/api/Product");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ProductListResponse>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (result?.Success == true && result.Data != null)
                    {
                        Products = result.Data.Where(p => p.IsAvailable).ToList();
                    }
                }
                else
                {
                    ApiError = "Không thể tải danh sách sản phẩm từ API vào lúc này.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to fetch products from WebAPI");
                ApiError = "Dịch vụ API hiện không khả dụng. Đang hiển thị nội dung tĩnh.";
            }
        }
    }
}
