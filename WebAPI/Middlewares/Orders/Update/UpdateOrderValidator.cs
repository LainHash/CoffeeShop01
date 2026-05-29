using FluentValidation;
using WebAPI.DTOs.Orders.Update;
using WebAPI.Helpers.Constants.Orders;

namespace WebAPI.Middlewares.Orders.Update
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderDTO>
    {
        private static readonly string[] AllowedStatuses = [InvoiceStatuses.Unpaid, InvoiceStatuses.Paid, InvoiceStatuses.Cancelled];

        public UpdateOrderValidator()
        {
            RuleFor(x => x.TableId)
                .GreaterThan(0).WithMessage("TableId không hợp lệ");

            RuleFor(x => x.EmployeePublicId)
                .NotEmpty().WithMessage("EmployeePublicId không được để trống");

            RuleFor(x => x.ReservationId)
                .GreaterThan(0).WithMessage("ReservationId không hợp lệ")
                .When(x => x.ReservationId.HasValue);

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Trạng thái đơn hàng không được để trống")
                .Must(s => AllowedStatuses.Contains(s))
                .WithMessage($"Trạng thái phải là: {string.Join(", ", AllowedStatuses)}");

            RuleFor(x => x.SubTotal)
                .GreaterThanOrEqualTo(0).WithMessage("Tạm tính không được âm");

            RuleFor(x => x.DiscountId)
                .GreaterThan(0).WithMessage("DiscountId không hợp lệ")
                .When(x => x.DiscountId.HasValue);

            RuleFor(x => x.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Tổng tiền không được âm");

            RuleFor(x => x.Note)
                .MaximumLength(500).WithMessage("Ghi chú tối đa 500 ký tự")
                .When(x => x.Note is not null);

            RuleFor(x => x.OrderDetails)
                .NotEmpty().WithMessage("Đơn hàng phải có ít nhất 1 sản phẩm");

            RuleForEach(x => x.OrderDetails)
                .SetValidator(new UpdateOrderDetailValidator());
        }
    }
}
