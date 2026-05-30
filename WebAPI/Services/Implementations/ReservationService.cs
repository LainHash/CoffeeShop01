using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs.Reservations;
using WebAPI.ResultModels;
using WebAPI.Models;
using WebAPI.Services.Interfaces;
using WebAPI.Helpers.Constants.Reservations;
using WebAPI.Helpers.Constants.Tables;

namespace WebAPI.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly CoffeeShopDbContext _context;
        private readonly IMapper _mapper;

        public ReservationService(CoffeeShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReservationResult<ReservationDTO>> CreateAsync(CreateReservationDTO dto)
        {
            if (dto.ReservationTime <= DateTime.Now.AddMinutes(30))
            {
                return new ReservationResult<ReservationDTO>
                {
                    Success = false,
                    Message = "Thời gian đặt bàn phải ít nhất 30 phút trong tương lai."
                };
            }

            if (dto.NumberOfGuests <= 0)
            {
                return new ReservationResult<ReservationDTO>
                {
                    Success = false,
                    Message = "Số lượng khách phải lớn hơn 0."
                };
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Phone == dto.Phone);

            if (customer == null)
            {
                customer = new Customer
                {
                    FullName = dto.FullName,
                    Phone = dto.Phone
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }
            else if (customer.FullName != dto.FullName)
            {
                customer.FullName = dto.FullName;
                _context.Customers.Update(customer);
            }

            var reservation = new Reservation
            {
                CustomerId = customer.CustomerId,
                TableId = 0,
                ReservationTime = dto.ReservationTime,
                NumberOfGuests = dto.NumberOfGuests,
                Note = dto.Note,
                Status = ReservationStatuses.Pending,
            };

            var conflictWindowStart = dto.ReservationTime.AddHours(-2);
            var conflictWindowEnd = dto.ReservationTime.AddHours(2);

            var busyTableIds = await _context.Reservations
                .Where(r => r.Status != ReservationStatuses.Cancelled
                         && r.ReservationTime >= conflictWindowStart
                         && r.ReservationTime <= conflictWindowEnd)
                .Select(r => r.TableId)
                .Distinct()
                .ToListAsync();

            var availableTableId = 0;

            if (dto.TableId.HasValue)
            {
                var table = await _context.TableEntities
                    .FirstOrDefaultAsync(t => t.TableId == dto.TableId.Value
                                            && t.IsActive == true
                                            && (t.Status == TableStatuses.Available
                                                || (t.Status != TableStatuses.Maintenance
                                                    && dto.ReservationTime >= t.UpdatedAt.AddHours(2))));

                if (table == null)
                {
                    return new ReservationResult<ReservationDTO>
                    {
                        Success = false,
                        Message = "Bàn được chọn không tồn tại hoặc không ở trạng thái sẵn sàng."
                    };
                }

                if (busyTableIds.Contains(table.TableId))
                {
                    return new ReservationResult<ReservationDTO>
                    {
                        Success = false,
                        Message = "Bàn được chọn đã có người đặt trong khung giờ này."
                    };
                }

                availableTableId = table.TableId;
            }
            else
            {
                var availableTable = await _context.TableEntities
                    .Where(t => t.IsActive == true
                             && (t.Status == TableStatuses.Available
                                 || (t.Status != TableStatuses.Maintenance
                                     && dto.ReservationTime >= t.UpdatedAt.AddHours(2)))
                             && t.RecommendedCapacity >= dto.NumberOfGuests
                             && !busyTableIds.Contains(t.TableId))
                    .OrderBy(t => t.RecommendedCapacity)
                    .FirstOrDefaultAsync();

                if (availableTable == null)
                    return new ReservationResult<ReservationDTO>
                    {
                        Success = false,
                        Message = "Hiện không có bàn trống phù hợp với số lượng khách trong khung giờ này. Vui lòng chọn giờ khác."
                    };

                availableTableId = availableTable.TableId;
            }

            reservation.TableId = availableTableId;



            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            await _context.Entry(reservation).Reference(r => r.Customer).LoadAsync();
            await _context.Entry(reservation).Reference(r => r.Table).LoadAsync();

            return new ReservationResult<ReservationDTO>
            {
                Success = true,
                Message = "Đặt bàn thành công! Chúng tôi sẽ xác nhận sớm.",
                Data = _mapper.Map<ReservationDTO>(reservation)
            };
        }

        public async Task<ReservationResult<List<ReservationDTO>>> GetByCustomerAsync(Guid customerPublicId)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.PublicId == customerPublicId);

            if (customer == null)
                return new ReservationResult<List<ReservationDTO>>
                {
                    Success = false,
                    Message = "Khách hàng không tồn tại."
                };

            var reservations = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Table)
                .Where(r => r.CustomerId == customer.CustomerId)
                .OrderByDescending(r => r.ReservationTime)
                .ToListAsync();

            return new ReservationResult<List<ReservationDTO>>
            {
                Success = true,
                Message = "Lấy danh sách đặt bàn thành công.",
                Data = _mapper.Map<List<ReservationDTO>>(reservations)
            };
        }

        public async Task<ReservationResult<List<ReservationDTO>>> GetAllAsync()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Table)
                .OrderByDescending(r => r.ReservationTime)
                .ToListAsync();

            return new ReservationResult<List<ReservationDTO>>
            {
                Success = true,
                Message = "Lấy tất cả đặt bàn thành công.",
                Data = _mapper.Map<List<ReservationDTO>>(reservations)
            };
        }

        public async Task<ReservationResult<ReservationDTO>> GetByIdAsync(Guid id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.PublicId == id);

            if (reservation == null)
                return new ReservationResult<ReservationDTO>
                {
                    Success = false,
                    Message = "Đặt bàn không tồn tại."
                };

            return new ReservationResult<ReservationDTO>
            {
                Success = true,
                Message = "Lấy thông tin đặt bàn thành công.",
                Data = _mapper.Map<ReservationDTO>(reservation)
            };
        }

        public async Task<ReservationResult<ReservationDTO>> UpdateAsync(Guid id, UpdateReservationDTO dto)
        {
            var allowedStatuses = new[] { "Pending", "Confirmed", "Cancelled", "Completed" };
            if (!allowedStatuses.Contains(dto.Status))
                return new ReservationResult<ReservationDTO>
                {
                    Success = false,
                    Message = $"Trạng thái không hợp lệ. Giá trị hợp lệ: {string.Join(", ", allowedStatuses)}."
                };

            var reservation = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.PublicId == id);

            if (reservation == null)
                return new ReservationResult<ReservationDTO>
                {
                    Success = false,
                    Message = "Đặt bàn không tồn tại."
                };

            if (dto.TableId.HasValue)
            {
                var table = await _context.TableEntities
                    .FirstOrDefaultAsync(t => t.TableId == dto.TableId.Value && t.IsActive == true);

                if (table == null)
                    return new ReservationResult<ReservationDTO>
                    {
                        Success = false,
                        Message = "Bàn không tồn tại hoặc không hoạt động."
                    };

                reservation.TableId = dto.TableId.Value;
            }

            if (dto.Note != null)
                reservation.Note = dto.Note;

            reservation.Status = dto.Status;
            await _context.SaveChangesAsync();

            await _context.Entry(reservation).Reference(r => r.Table).LoadAsync();

            return new ReservationResult<ReservationDTO>
            {
                Success = true,
                Message = "Cập nhật đặt bàn thành công.",
                Data = _mapper.Map<ReservationDTO>(reservation)
            };
        }

        public async Task<ReservationResult<List<ReservationDTO>>> GetByWeekAsync(DateTime weekStart)
        {
            var startOfWeek = weekStart.Date;
            var endOfWeek = startOfWeek.AddDays(7);

            var reservations = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Table)
                .Where(r => r.ReservationTime >= startOfWeek && r.ReservationTime < endOfWeek)
                .OrderBy(r => r.ReservationTime)
                .ToListAsync();

            return new ReservationResult<List<ReservationDTO>>
            {
                Success = true,
                Message = "Lấy lịch đặt bàn theo tuần thành công.",
                Data = _mapper.Map<List<ReservationDTO>>(reservations)
            };
        }
    }
}
