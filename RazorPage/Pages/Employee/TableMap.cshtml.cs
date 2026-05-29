using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.DTOs.Manager;
using RazorPage.Helpers.Constants.Orders;
using System.Text;
using System.Text.Json;

namespace RazorPage.Pages.Employee
{
    public class TableMapModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TableMapModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<TableEntityDTO> Tables { get; set; } = new();
        
        [TempData]
        public string? SuccessMessage { get; set; }
        
        [TempData]
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
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
                if (tableJson?.Data != null)
                {
                    Tables = tableJson.Data;
                }
            }
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int tableId, string status)
        {
            var validStatuses = new[] { TableStatuses.Available, TableStatuses.Occupied, TableStatuses.Reserved, TableStatuses.Unavailable };
            if (!validStatuses.Contains(status))
            {
                ErrorMessage = "Trạng thái không hợp lệ.";
                return RedirectToPage();
            }

            var client = _httpClientFactory.CreateClient("WebAPI");
            var payload = new { Status = status };
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/Table/{tableId}/status", content);
            if (response.IsSuccessStatusCode)
            {
                SuccessMessage = "Cập nhật trạng thái bàn thành công.";
            }
            else
            {
                ErrorMessage = "Lỗi khi cập nhật trạng thái bàn.";
            }

            return RedirectToPage();
        }
    }
}
