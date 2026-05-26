using FluentValidation;
using WebAPI.DTOs.Accounts.Managers.Update;

namespace WebAPI.Middlewares.Accounts.Managers
{
    public class UpdateManagerInfoValidator : AbstractValidator<UpdateManagerInfoDTO>
    {
        public UpdateManagerInfoValidator()
        {
            RuleFor(x => x.FullName)
                .MaximumLength(100).WithMessage("Họ tên tối đa 100 ký tự")
                .When(x => x.FullName is not null);

            RuleFor(x => x.Phone)
                .Matches(@"^(0|\+84)[0-9]{8,10}$").WithMessage("Số điện thoại không đúng định dạng")
                .When(x => !string.IsNullOrWhiteSpace(x.Phone));

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email không đúng định dạng")
                .MaximumLength(256).WithMessage("Email tối đa 256 ký tự")
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.Position)
                .MaximumLength(50).WithMessage("Chức vụ tối đa 50 ký tự")
                .When(x => x.Position is not null);
        }
    }
}
