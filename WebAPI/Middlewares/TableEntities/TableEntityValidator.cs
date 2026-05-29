using FluentValidation;
using WebAPI.DTOs.TableEntities;

namespace WebAPI.Middlewares.TableEntities
{
    public class TableEntityValidator : AbstractValidator<TableEntityDTO>
    {
        private static readonly string[] AllowedShapes = ["Round", "Square", "Rectangle"];
        private static readonly string[] AllowedStatuses = ["Available", "Occupied", "Reserved", "Maintenance"];

        public TableEntityValidator()
        {
            RuleFor(x => x.Shape)
                .NotEmpty().WithMessage("Hình dạng bàn không được để trống")
                .Must(s => AllowedShapes.Contains(s))
                .WithMessage($"Hình dạng bàn phải là: {string.Join(", ", AllowedShapes)}");

            RuleFor(x => x.TableNumber)
                .GreaterThan(0).WithMessage("Số bàn phải lớn hơn 0");

            RuleFor(x => x.FloorNumber)
                .GreaterThan(0).WithMessage("Số tầng phải lớn hơn 0");

            RuleFor(x => x.RecommendedCapacity)
                .GreaterThan(0).WithMessage("Sức chứa khuyến nghị phải lớn hơn 0")
                .LessThanOrEqualTo(x => x.MaxCapacity)
                .WithMessage("Sức chứa khuyến nghị không được vượt quá sức chứa tối đa");

            RuleFor(x => x.MaxCapacity)
                .GreaterThan(0).WithMessage("Sức chứa tối đa phải lớn hơn 0");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Trạng thái bàn không được để trống")
                .Must(s => AllowedStatuses.Contains(s))
                .WithMessage($"Trạng thái bàn phải là: {string.Join(", ", AllowedStatuses)}");
        }
    }
}
