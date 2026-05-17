using AutoMapper;
using WebAPI.DTOs.Accounts.Customers;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class CustomerMP : Profile
    { 
        public CustomerMP() {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<RegisterDTO, Customer>();
            CreateMap<RegisterDTO, User>();
            CreateMap<User, AccountDTO>();
        }
    }
}
