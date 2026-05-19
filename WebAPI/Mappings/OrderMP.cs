using AutoMapper;
using WebAPI.DTOs.Orders;
using WebAPI.Models;
using WebAPI.DTOs.Orders.Create;

namespace WebAPI.Mappings
{
    public class OrderMP : Profile
    {
        public OrderMP() { 
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDetail, OrderDetailDTO>();
            CreateMap<CreateOrderDTO, Order>();
            CreateMap<CreateOrderDetailDTO, OrderDetail>();
        }
    }
}
