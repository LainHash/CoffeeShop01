using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RazorPage.DTOs.Manager
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public Guid PublicId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UnitsInstock { get; set; }
    }

    public class CreateProductDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập URL hình ảnh")]
        public string ImageUrl { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        public int UnitsInstock { get; set; }
    }

    public class UpdateProductDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập URL hình ảnh")]
        public string ImageUrl { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        public int UnitsInstock { get; set; }
    }

    public class ProductListApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<ProductDTO> List { get; set; } = new();
    }

    public class ProductApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public ProductDTO? Product { get; set; }
    }

    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    public class CategoryListApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<CategoryDTO> List { get; set; } = new();
    }
}
