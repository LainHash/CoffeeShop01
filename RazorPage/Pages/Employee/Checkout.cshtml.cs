using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using RazorPage.DTOs.Manager;

namespace RazorPage.Pages.Employee
{
    public class CheckoutModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CheckoutModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public OrderDTO? Order { get; set; }
        public string? ErrorMessage { get; set; }

        [BindProperty]
        public string PaymentMethod { get; set; } = "Cash";

        [BindProperty]
        public string? Note { get; set; }

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

            if (Order.Status != "Unpaid")
            {
                return RedirectToPage("/Order/Details", new { id = Order.PublicId });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var managerIdStr = HttpContext.Session.GetString("ManagerId");
            if (string.IsNullOrEmpty(managerIdStr))
            {
                return RedirectToPage("/Auth/Login");
            }

            var client = _httpClientFactory.CreateClient("WebAPI");
            try
            {
                // Call Checkout with confirm=true and pass payment method + notes
                var response = await client.PostAsync(
                    $"/api/Order/{id}/Checkout?confirm=true&paymentMethod={Uri.EscapeDataString(PaymentMethod)}&note={Uri.EscapeDataString(Note ?? "")}", 
                    null
                );

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Order/Details", new { id = id });
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    ErrorMessage = "Thanh toán thất bại: " + errorMsg;
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
                    ErrorMessage = "Không thể tìm thấy hóa đơn cần thanh toán.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Lỗi kết nối hệ thống API: " + ex.Message;
            }
        }
    }
}
