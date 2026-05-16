using WebAPI.DTOs.Categories;

namespace WebAPI.DTOs.Results
{
    public class CategoryResult
    {
        public bool Sucess { get; set; }
        public string? Message { get; set; }
        public CategoryDTO? Category { get; set; }

        public CategoryResult(bool success, string message)
        {
            Sucess = success;
            Message = message;
        }

        public CategoryResult(bool success, string message, CategoryDTO dto)
        {
            Sucess = success;
            Message = message;
            Category = dto;
        }
    }
}
