using AutoMapper;
using WebAPI.DTOs.Reservations;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class ReservationMP : Profile
    {
        public ReservationMP()
        {
            CreateMap<Reservation, ReservationDTO>()
                .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.Customer.FullName))
                .ForMember(dest => dest.CustomerPhone,    opt => opt.MapFrom(src => src.Customer.Phone))
                .ForMember(dest => dest.TableName,        opt => opt.MapFrom(src => src.Table != null ? src.Table.TableName : null))
                .ForMember(dest => dest.AreaName,         opt => opt.MapFrom(src => src.Table != null && src.Table.Area != null ? src.Table.Area.AreaName : null));
        }
    }
}
