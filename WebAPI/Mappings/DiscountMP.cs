using AutoMapper;
using WebAPI.DTOs.Discounts;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class DiscountMP : Profile
    {
        public DiscountMP()
        {
            CreateMap<Discount, DiscountDTO>();
        }
    }
}
