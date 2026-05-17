namespace WebAPI.DTOs.TableEntities
{
    public class TableEntityDTO
    {

        public string Shape { get; set; } = null!;

        public int TableNumber { get; set; }

        public int FloorNumber { get; set; }

        public int RecommendedCapacity { get; set; }

        public int MaxCapacity { get; set; }

        public string Status { get; set; } = null!;
    }
}
