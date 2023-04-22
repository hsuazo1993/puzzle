namespace DTOs
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public EventDto Event { get; set; } = default!;
        public ClientDto Client { get; set; } = default!;
        public List<FurnitureDto> Furnitures { get; set; } = default!;
    }

    public class EventDto
    {
        public int Id { get; set; }
        public EventTypeDto Type { get; set; } = default!;
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
    public class EventTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsEnabled { get; set; } = true;
    }

    public class FurnitureDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsReserved { get; set; } = false;
    }
}
