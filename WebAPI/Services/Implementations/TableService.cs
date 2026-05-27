using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.ResultModels;
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

        public async Task<TableResult<List<TableEntityDTO>>> GetAllAsync()
        {
            var tables = await _context.TableEntities
                .ToListAsync();
            return new TableResult<List<TableEntityDTO>>
            {
                Success = true,
                Message = "Lấy danh sách bàn thành công",
                Data = _mapper.Map<List<TableEntityDTO>>(tables)
            };
        }

        public async Task<TableResult<TableEntityDTO>> GetOneAsync(int floorNumber, int tableNumber)
        {
            var table = await _context.TableEntities
                .FirstOrDefaultAsync(t => t.TableNumber == tableNumber && t.FloorNumber == floorNumber);
            if (table == null)
            {
                return new TableResult<TableEntityDTO>
                {
                    Success = false,
                    Message = "Bàn không tồn tại!"
                };
            }
            return new TableResult<TableEntityDTO>
            {
                Success = true,
                Message = "Lấy bàn thành công.",
                Data = _mapper.Map<TableEntityDTO>(table)
            };
        }

        public async Task<TableResult<List<TableEntityDTO>>> GetAllByFloorAsync(int floorNumber)
        {
            var tables = await _context.TableEntities
                .Where(t => t.FloorNumber == floorNumber)
                .ToListAsync();
            return new TableResult<List<TableEntityDTO>>
            {
                Success = true,
                Message = "Lấy danh sách bàn theo tầng thành công",
                Data = _mapper.Map<List<TableEntityDTO>>(tables)
            };
        }

        public async Task<TableResult<TableEntityDTO>> UpdateStatusAsync(int floorNumber, int tableNumber, string status)
        {
            var table = await _context.TableEntities
                .FirstOrDefaultAsync(t => t.FloorNumber == floorNumber && t.TableNumber == tableNumber);
            if (table == null)
            {
                return new TableResult<TableEntityDTO>
                {
                    Success = false,
                    Message = "Không tìm thấy bàn!"
                };
            }

            table.Status = status;
            _context.TableEntities.Update(table);
            await _context.SaveChangesAsync();

            return new TableResult<TableEntityDTO>
            {
                Success = true,
                Message = "Cập nhật trạng thái bàn thành công",
                Data = _mapper.Map<TableEntityDTO>(table)
            };
        }
    }
}
