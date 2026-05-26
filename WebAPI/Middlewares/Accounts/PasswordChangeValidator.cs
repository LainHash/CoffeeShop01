using FluentValidation;
using WebAPI.DTOs.Accounts;

namespace WebAPI.Middlewares.Accounts
{
    public class PasswordChangeValidator : AbstractValidator<PasswordChangeDTO>
    {
        public PasswordChangeValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Mật khẩu hiện tại không được để trống");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Mật khẩu mới không được để trống")
                .MinimumLength(6).WithMessage("Mật khẩu mới tối thiểu 6 ký tự")
                .MaximumLength(100).WithMessage("Mật khẩu mới tối đa 100 ký tự")
                .NotEqual(x => x.CurrentPassword).WithMessage("Mật khẩu mới không được trùng mật khẩu hiện tại");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage("Xác nhận mật khẩu không được để trống")
                .Equal(x => x.NewPassword).WithMessage("Xác nhận mật khẩu không khớp với mật khẩu mới");
        }
    }
}
