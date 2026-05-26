using FluentValidation;
using WebAPI.DTOs.Orders.Create;

namespace WebAPI.Middlewares.Orders.Create
{
    public class CreateOrderDetailValidator : AbstractValidator<CreateOrderDetailDTO>
    {
        public CreateOrderDetailValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ProductId không hợp lệ");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Số lượng phải lớn hơn 0");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Đơn giá phải lớn hơn 0");

            RuleFor(x => x.LineTotal)
                .GreaterThanOrEqualTo(0).WithMessage("Thành tiền không được âm");
        }
    }
}
