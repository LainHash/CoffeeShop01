using FluentValidation;
using WebAPI.DTOs.Orders.Update;

namespace WebAPI.Middlewares.Orders.Update
{
    public class UpdateOrderDetailValidator : AbstractValidator<UpdateOrderDetailDTO>
    {
        public UpdateOrderDetailValidator()
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
