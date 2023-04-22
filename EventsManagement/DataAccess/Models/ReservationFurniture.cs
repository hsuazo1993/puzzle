namespace DataAccess.Models
{
    public class ReservationFurniture
    {
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; } = default!;
        public int FurnitureId { get; set; }
        public Furniture Furniture { get; set; } = default!;
    }
}
