using FluentValidation;
using WebAPI.DTOs.Discounts;

namespace WebAPI.Middlewares.Discounts
{
    public class DiscountValidator : AbstractValidator<DiscountDTO>
    {
        private static readonly string[] AllowedTypes = ["Percentage", "Fixed"];

        public DiscountValidator()
        {
            RuleFor(x => x.DiscountCode)
                .MaximumLength(50).WithMessage("Mã giảm giá tối đa 50 ký tự")
                .When(x => x.DiscountCode is not null);

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Loại giảm giá không được để trống")
                .Must(t => AllowedTypes.Contains(t))
                .WithMessage($"Loại giảm giá phải là: {string.Join(", ", AllowedTypes)}");

            RuleFor(x => x.Value)
                .GreaterThan(0).WithMessage("Giá trị giảm giá phải lớn hơn 0")
                .LessThanOrEqualTo(100).WithMessage("Giảm giá theo % không được vượt quá 100")
                .When(x => x.Type == "Percentage");

            RuleFor(x => x.Value)
                .GreaterThan(0).WithMessage("Giá trị giảm giá phải lớn hơn 0")
                .When(x => x.Type == "Fixed");

            RuleFor(x => x.ExpiredDate)
                .GreaterThan(DateTime.Now).WithMessage("Ngày hết hạn phải ở tương lai")
                .When(x => x.ExpiredDate.HasValue);
        }
    }
}
