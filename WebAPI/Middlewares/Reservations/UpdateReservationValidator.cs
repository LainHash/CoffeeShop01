using FluentValidation;
using WebAPI.DTOs.Reservations;

namespace WebAPI.Middlewares.Reservations
{
    public class UpdateReservationValidator : AbstractValidator<UpdateReservationDTO>
    {
        private static readonly string[] AllowedStatuses = ["Pending", "Confirmed", "Cancelled", "Completed"];

        public UpdateReservationValidator()
        {
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Trạng thái không được để trống")
                .Must(s => AllowedStatuses.Contains(s))
                .WithMessage($"Trạng thái phải là: {string.Join(", ", AllowedStatuses)}");

            RuleFor(x => x.TableId)
                .GreaterThan(0).WithMessage("TableId không hợp lệ")
                .When(x => x.TableId.HasValue);

            RuleFor(x => x.Note)
                .MaximumLength(500).WithMessage("Ghi chú tối đa 500 ký tự")
                .When(x => x.Note is not null);
        }
    }
}
