using AutoMapper;
using WebAPI.DTOs.Accounts.Managers;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class ManagerMP : Profile
    {
        public ManagerMP() {

            CreateMap<CreateEmployeeDTO, Employee>();
            CreateMap<CreateEmployeeDTO, User>();

            CreateMap<Employee, ManagerDTO>();
            CreateMap<User, ManagerDTO>();
        }
    }
}
