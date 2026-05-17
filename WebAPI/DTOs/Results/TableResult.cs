using WebAPI.DTOs.Reservations;
using WebAPI.DTOs.TableEntities;

namespace WebAPI.DTOs.Results
{
    public class TableResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public TableEntityDTO? TableEntity { get; set; }
        public List<TableEntityDTO>? TableEntities { get; set; }

        public TableResult(bool succes, string message)
        {
            Success = succes;
            Message = message;
        }

        public TableResult(bool success, string message,
            TableEntityDTO? table = null)
        {
            Success = success;
            Message = message;
            TableEntity = table;
        }

        public TableResult(bool success, string message,
            List<TableEntityDTO>? tables = null)
        {
            Success = success;
            Message = message;
            TableEntities = tables;
        }
    }
}
