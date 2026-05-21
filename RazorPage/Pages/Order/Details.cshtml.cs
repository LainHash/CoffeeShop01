using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using RazorPage.DTOs.Manager;

namespace RazorPage.Pages.Order
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public OrderDTO? Order { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            var managerIdStr = HttpContext.Session.GetString("ManagerId");
            if (string.IsNullOrEmpty(managerIdStr))
            {
                return RedirectToPage("/Auth/Login");
            }

            if (id == null)
            {
                return RedirectToPage("/Manager/Orders");
            }

            await LoadOrder(id.Value);

            if (Order == null)
            {
                return RedirectToPage("/Manager/Orders");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostCancelAsync(Guid id)
        {
            var managerIdStr = HttpContext.Session.GetString("ManagerId");
            if (string.IsNullOrEmpty(managerIdStr))
            {
                return RedirectToPage("/Auth/Login");
            }

            var client = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                // Call Checkout with confirm=false to cancel the order
                var response = await client.PostAsync($"/api/Order/{id}/Checkout?confirm=false", null);
                if (response.IsSuccessStatusCode)
                {
                    SuccessMessage = "Hóa đơn đã được hủy thành công.";
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    ErrorMessage = "Không thể hủy hóa đơn: " + errorMsg;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Đã xảy ra lỗi kết nối: " + ex.Message;
            }

            await LoadOrder(id);
            return Page();
        }

        private async Task LoadOrder(Guid id)
        {
            var client = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                var response = await client.GetAsync($"/api/Order/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<OrderResult>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (result?.Success == true)
                    {
                        Order = result.Order;
                    }
                }
                else
                {
                    ErrorMessage = "Không thể tìm thấy hóa đơn.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Lỗi kết nối đến hệ thống API: " + ex.Message;
            }
        }
    }
}
