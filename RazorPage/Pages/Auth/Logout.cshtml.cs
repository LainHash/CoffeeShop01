using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Chỉ xóa session phía RazorPage — API không cần biết
            HttpContext.Session.Clear();

            TempData["SuccessMessage"] = "Bạn đã đăng xuất thành công.";
            return RedirectToPage("/Auth/Login");
        }
    }
}
