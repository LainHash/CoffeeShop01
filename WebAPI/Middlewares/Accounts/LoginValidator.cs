using FluentValidation;
using WebAPI.DTOs.Accounts;

namespace WebAPI.Middlewares.Accounts
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Email không đúng định dạng")
                .MaximumLength(256).WithMessage("Email tối đa 256 ký tự");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống")
                .MinimumLength(6).WithMessage("Mật khẩu tối thiểu 6 ký tự")
                .MaximumLength(100).WithMessage("Mật khẩu tối đa 100 ký tự");
        }
    }
}
