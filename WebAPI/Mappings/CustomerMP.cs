using AutoMapper;
using WebAPI.DTOs.Accounts.Customers;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class CustomerMP : Profile
    {
        public CustomerMP()
        {
            // Customer -> CustomerDTO (plain data, no User link)
            CreateMap<Customer, CustomerDTO>();
        }
    }
}
