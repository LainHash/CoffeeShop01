using WebAPI.DTOs.Reservations;
using WebAPI.DTOs.Results;

namespace WebAPI.Services.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationResult> CreateAsync(CreateReservationDTO dto);

        Task<ReservationResult> GetByCustomerAsync(Guid customerPublicId);

        Task<ReservationResult> GetAllAsync();

        Task<ReservationResult> GetByIdAsync(int reservationId);

        Task<ReservationResult> UpdateAsync(int reservationId, UpdateReservationDTO dto);

        Task<ReservationResult> GetByWeekAsync(DateTime weekStart);
    }
}
