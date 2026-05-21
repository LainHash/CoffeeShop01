using AutoMapper;
using WebAPI.DTOs.TableEntities;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class TableEntityMP : Profile
    {
        public TableEntityMP() {
            CreateMap<TableEntity, TableEntityDTO>();
        }
    }
}
