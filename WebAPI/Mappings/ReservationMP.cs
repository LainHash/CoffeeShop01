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
                .ForMember(dest => dest.TableNumber,     opt => opt.MapFrom(src => src.Table != null ? src.Table.TableNumber : 0))
                .ForMember(dest => dest.FloorNumber,     opt => opt.MapFrom(src => src.Table != null ? src.Table.FloorNumber : 0));
        }
    }
}
