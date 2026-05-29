using FluentValidation;
using WebAPI.DTOs.Accounts.Customers.Update;

namespace WebAPI.Middlewares.Accounts.Customers
{
    public class UpdateCustomerInfoValidator : AbstractValidator<UpdateInfoDTO>
    {
        public UpdateCustomerInfoValidator()
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
        }
    }
}
