namespace DataAccess.Models
{
    public class Furniture
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsReserved { get; set; } = false;
        public virtual ICollection<ReservationFurniture> ReservationsFurnitures { get; set; } = default!;
    }
}
