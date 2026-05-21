using WebAPI.DTOs.Categories;

namespace WebAPI.DTOs.Results
{
    public class CategoryResult : ResultBase
    {
        public CategoryDTO? Category { get; set; }
        public List<CategoryDTO>? Categories { get; set; }

        public CategoryResult(bool success, string message) : base(success, message) { }


        public CategoryResult(bool success, string message, CategoryDTO dto) : base(success, message)
        {
            Category = dto;
        }

        public CategoryResult(bool success, string message, List<CategoryDTO> categories) : base(success, message)
        {
            Categories = categories;
        }
    }
}
