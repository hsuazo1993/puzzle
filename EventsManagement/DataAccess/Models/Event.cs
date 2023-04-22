namespace DataAccess.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public EventType Type { get; set; } = default!;
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; } = default!;
    }
}
