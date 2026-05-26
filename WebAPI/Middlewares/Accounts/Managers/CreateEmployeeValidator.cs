using FluentValidation;
using WebAPI.DTOs.Accounts.Managers;

namespace WebAPI.Middlewares.Accounts.Managers
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDTO>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Email không đúng định dạng")
                .MaximumLength(256).WithMessage("Email tối đa 256 ký tự");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Số điện thoại không được để trống")
                .Matches(@"^(0|\+84)[0-9]{8,10}$").WithMessage("Số điện thoại không đúng định dạng");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Tên đăng nhập không được để trống")
                .MinimumLength(3).WithMessage("Tên đăng nhập tối thiểu 3 ký tự")
                .MaximumLength(50).WithMessage("Tên đăng nhập tối đa 50 ký tự")
                .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("Tên đăng nhập chỉ được chứa chữ cái, số và dấu gạch dưới");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống")
                .MinimumLength(6).WithMessage("Mật khẩu tối thiểu 6 ký tự")
                .MaximumLength(100).WithMessage("Mật khẩu tối đa 100 ký tự");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Xác nhận mật khẩu không được để trống")
                .Equal(x => x.Password).WithMessage("Xác nhận mật khẩu không khớp");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Họ tên không được để trống")
                .MaximumLength(100).WithMessage("Họ tên tối đa 100 ký tự");

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Chức vụ không được để trống")
                .MaximumLength(50).WithMessage("Chức vụ tối đa 50 ký tự");
        }
    }
}
