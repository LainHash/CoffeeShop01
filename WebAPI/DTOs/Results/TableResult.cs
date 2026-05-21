using WebAPI.DTOs.Reservations;
using WebAPI.DTOs.TableEntities;

namespace WebAPI.DTOs.Results
{
    public class TableResult : ResultBase
    {
        public TableEntityDTO? TableEntity { get; set; }
        public List<TableEntityDTO>? TableEntities { get; set; }

        public TableResult(bool succes, string message) : base(succes, message)
        {
        }

        public TableResult(bool success, string message,
            TableEntityDTO? table = null) : base(success, message)
        {
            TableEntity = table;
        }

        public TableResult(bool success, string message,
            List<TableEntityDTO>? tables = null) : base(success, message)
        {
            TableEntities = tables;
        }
    }
}
