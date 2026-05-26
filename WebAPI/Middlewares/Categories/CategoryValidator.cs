using FluentValidation;
using WebAPI.DTOs.Categories;

namespace WebAPI.Middlewares.Categories
{
    public class CategoryValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Tên danh mục không được để trống")
                .MaximumLength(100).WithMessage("Tên danh mục tối đa 100 ký tự");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả tối đa 500 ký tự")
                .When(x => x.Description is not null);
        }
    }
}
