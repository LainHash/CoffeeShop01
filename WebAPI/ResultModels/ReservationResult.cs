using WebAPI.DTOs.Reservations;

namespace WebAPI.ResultModels
{
    public class ReservationResult<T> : BaseResult
    {
        public T? Data { get; set; }
    }
}
