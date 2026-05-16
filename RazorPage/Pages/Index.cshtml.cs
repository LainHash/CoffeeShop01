using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace RazorPage.Pages
{
    public class ProductDto
    {
        public Guid PublicId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UnitsInstock { get; set; }
    }

    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<IndexModel> _logger;

        public List<ProductDto> Products { get; set; } = new();
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

                    if (result?.Success == true && result.List != null)
                    {
                        Products = result.List.Where(p => p.IsAvailable).ToList();
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

    internal class ProductListResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<ProductDto>? List { get; set; }
    }
}
