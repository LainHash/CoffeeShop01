using FluentValidation;
using WebAPI.DTOs.Reservations;

namespace WebAPI.Middlewares.Reservations
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationDTO>
    {
        public CreateReservationValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Họ tên khách hàng không được để trống")
                .MaximumLength(100).WithMessage("Họ tên tối đa 100 ký tự");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Số điện thoại không được để trống")
                .Matches(@"^(0|\+84)[0-9]{8,10}$").WithMessage("Số điện thoại không đúng định dạng");

            RuleFor(x => x.ReservationTime)
                .GreaterThan(DateTime.Now).WithMessage("Thời gian đặt bàn phải ở tương lai");

            RuleFor(x => x.NumberOfGuests)
                .GreaterThan(0).WithMessage("Số lượng khách phải lớn hơn 0")
                .LessThanOrEqualTo(100).WithMessage("Số lượng khách tối đa 100 người");

            RuleFor(x => x.Note)
                .MaximumLength(500).WithMessage("Ghi chú tối đa 500 ký tự")
                .When(x => x.Note is not null);

            RuleFor(x => x.TableId)
                .GreaterThan(0).WithMessage("TableId không hợp lệ")
                .When(x => x.TableId.HasValue);
        }
    }
}
