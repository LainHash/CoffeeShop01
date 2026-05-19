using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;

namespace RazorPage.Pages.Manager
{
    public class POSModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public POSModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<TableEntityDTO> Tables { get; set; } = new();
        public List<ProductDTO> Products { get; set; } = new();

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadData();
            return Page();
        }

        [BindProperty]
        public CreateOrderInput Input { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (Input.TableId == 0)
            {
                ErrorMessage = "Vui lòng chọn bàn.";
                await LoadData();
                return Page();
            }

            if (Input.OrderDetails == null || !Input.OrderDetails.Any())
            {
                ErrorMessage = "Vui lòng thêm sản phẩm vào đơn hàng.";
                await LoadData();
                return Page();
            }




            Input.Status = "Pending";

            var client = _httpClientFactory.CreateClient("WebAPI");
            var json = JsonSerializer.Serialize(Input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/Order", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                SuccessMessage = "Tạo hóa đơn thành công!";
                Input = new CreateOrderInput();
            }
            else
            {
                ErrorMessage = "Lỗi khi tạo hóa đơn: " + responseBody;
            }

            await LoadData();
            return Page();
        }

        private async Task LoadData()
        {
            var client = _httpClientFactory.CreateClient("WebAPI");

            var tableResp = await client.GetAsync("/api/Table");
            if (tableResp.IsSuccessStatusCode)
            {
                var tableStr = await tableResp.Content.ReadAsStringAsync();
                var tableJson = JsonSerializer.Deserialize<TableResponse>(tableStr, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (tableJson?.List != null) Tables = tableJson.List;
            }

            var prodResp = await client.GetAsync("/api/Product");
            if (prodResp.IsSuccessStatusCode)
            {
                var prodStr = await prodResp.Content.ReadAsStringAsync();
                var prodJson = JsonSerializer.Deserialize<ProductResponse>(prodStr, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (prodJson?.List != null) Products = prodJson.List;
            }
        }
    }

    public class CreateOrderInput
    {
        public int TableId { get; set; }
        public int EmployeeId { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal DiscountAmount { get; set; } = 0;
        public List<CreateOrderDetailInput> OrderDetails { get; set; } = new();
    }

    public class CreateOrderDetailInput
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class TableResponse
    {
        public List<TableEntityDTO> List { get; set; } = new();
    }

    public class TableEntityDTO
    {
        public int TableId { get; set; }
        public string Shape { get; set; } = "";
        public int TableNumber { get; set; }
        public int FloorNumber { get; set; }
    }

    public class ProductResponse
    {
        public List<ProductDTO> List { get; set; } = new();
    }

    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = "";
    }
}
