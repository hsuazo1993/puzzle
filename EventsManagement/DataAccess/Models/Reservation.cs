namespace DataAccess.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; } = default!;
        public int ClientId { get; set; }
        public Client Client { get; set; } = default!;
        public ICollection<ReservationFurniture> ReservationsFurnitures { get; set; } = default!;
    }
}
