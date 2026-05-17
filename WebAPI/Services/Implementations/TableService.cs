using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Results;
using WebAPI.DTOs.TableEntities;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Implementations
{
    public class TableService : ITableService
    {
        private readonly CoffeeShopDbContext _context;
        private readonly IMapper _mapper;

        public TableService(CoffeeShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TableResult> GetAllAsync()
        {
            var tables = await _context.TableEntities
                .ToListAsync();
            return new TableResult(true, "Lấy danh sách bàn thành công", _mapper.Map<List<TableEntityDTO>>(tables));
        }

        public async Task<TableResult> GetOneAsync(int tableNumber, int floorNumber)
        {
            var table = await _context.TableEntities
                .FirstOrDefaultAsync(t => t.TableNumber == tableNumber && t.FloorNumber == floorNumber);
            if(table == null)
            {
                return new TableResult(false, "Bàn không tồn tại!");
            }
            return new TableResult(true, "Lấy bàn thành công.", _mapper.Map<TableEntityDTO>(table));
        }
    }
}
