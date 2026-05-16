using System.ComponentModel.DataAnnotations;

namespace RazorPage.DTOs.Accounts.Customers
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; } = null!;
    }
}
