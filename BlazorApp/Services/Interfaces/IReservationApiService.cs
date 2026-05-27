using BlazorApp.Models.Reservations;

namespace BlazorApp.Services.Interfaces
{
    public interface IReservationApiService
    {
        Task<ReservationListResponse?> GetAllAsync();
        Task<ReservationResponse?> GetByIdAsync(Guid id);
        Task<ReservationResponse?> CreateAsync(CreateReservationInput input);
        Task<ReservationResponse?> UpdateAsync(Guid id, UpdateReservationInput input);
    }
}
