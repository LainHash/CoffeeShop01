using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using RazorPage.DTOs.Manager;

namespace RazorPage.Pages.Employee
{
    public class OrdersModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrdersModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<OrderDTO> Orders { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var managerIdStr = HttpContext.Session.GetString("ManagerId");
            if (string.IsNullOrEmpty(managerIdStr))
            {
                return RedirectToPage("/Auth/Login");
            }

            var client = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                var response = await client.GetAsync("/api/Order");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<OrderResult>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (result?.Success == true && result.Orders != null)
                    {
                        Orders = result.Orders;
                        // Sort orders: Unpaid first, then newest first
                        Orders.Sort((x, y) =>
                        {
                            if (x.Status == "Unpaid" && y.Status != "Unpaid") return -1;
                            if (x.Status != "Unpaid" && y.Status == "Unpaid") return 1;
                            return y.OrderId.CompareTo(x.OrderId);
                        });
                    }
                }
                else
                {
                    ErrorMessage = "Không thể lấy danh sách hóa đơn từ hệ thống.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Đã xảy ra lỗi kết nối: " + ex.Message;
            }

            return Page();
        }
    }
}
