using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using RazorPage.DTOs.Manager;
using RazorPage.Helpers.Constants.Orders;

namespace RazorPage.Pages.Employee
{
    public class POSModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public POSModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<TableEntityDTO> Tables { get; set; } = new();
        public List<POSProductDTO> Products { get; set; } = new();
        public List<POSDiscountDTO> Discounts { get; set; } = new();

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery] int? tableId)
        {
            await LoadData();
            if (tableId.HasValue)
            {
                Input.TableId = tableId.Value;
            }
            return Page();
        }

        [BindProperty]
        public CreateOrderInput Input { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            var managerIdStr = HttpContext.Session.GetString("ManagerId");
            if (string.IsNullOrEmpty(managerIdStr) || !Guid.TryParse(managerIdStr, out Guid managerId))
            {
                ErrorMessage = "Không tìm thấy thông tin nhân viên (chưa đăng nhập hoặc phiên hết hạn).";
                await LoadData();
                return Page();
            }

            Input.EmployeePublicId = managerId;

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

            Input.Status = InvoiceStatuses.Unpaid;

            var client = _httpClientFactory.CreateClient("WebAPI");
            var json = JsonSerializer.Serialize(Input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/Order", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<OrderResult>(responseBody, options);
                if (result?.Order?.PublicId != null)
                {
                    return RedirectToPage("/Order/Details", new { id = result.Order.PublicId });
                }
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

            var discountResp = await client.GetAsync("/api/Discount");
            if (discountResp.IsSuccessStatusCode)
            {
                var discountStr = await discountResp.Content.ReadAsStringAsync();
                var discountJson = JsonSerializer.Deserialize<DiscountResponse>(discountStr, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (discountJson?.List != null) Discounts = discountJson.List;
            }
        }
    }
}
