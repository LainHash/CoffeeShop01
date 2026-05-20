using AutoMapper;
using WebAPI.DTOs.Orders;
using WebAPI.Models;
using WebAPI.DTOs.Orders.Create;

namespace WebAPI.Mappings
{
    public class OrderMP : Profile
    {
        public OrderMP() { 
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.TableNumber, opt => opt.MapFrom(src => src.Table != null ? src.Table.TableNumber : 0))
                .ForMember(dest => dest.FloorNumber, opt => opt.MapFrom(src => src.Table != null ? src.Table.FloorNumber : 0))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.FullName : ""));

            CreateMap<OrderDetail, OrderDetailDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : ""))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product != null ? src.Product.ImageUrl : ""));

            CreateMap<CreateOrderDTO, Order>()
                .ForMember(dest => dest.EmployeeId, opt => opt.Ignore());
            CreateMap<CreateOrderDetailDTO, OrderDetail>();
        }
    }
}
