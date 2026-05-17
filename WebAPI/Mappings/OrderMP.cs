using AutoMapper;
using WebAPI.DTOs.Orders;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class OrderMP : Profile
    {
        public OrderMP() { 
            CreateMap<Order, OrderDTO>();
        }
    }
}
