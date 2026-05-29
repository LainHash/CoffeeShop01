using WebAPI.DTOs.Reservations;
using WebAPI.DTOs.TableEntities;

namespace WebAPI.ResultModels
{
    public class TableResult : BaseResult
    {

    }
    public class TableResult<T> : BaseResult
    {
        public T? Data { get; set; }
    }
}
