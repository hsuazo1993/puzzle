﻿namespace DataAccess.Models
{
    public class EventType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsEnabled { get; set; } = true;
        public virtual ICollection<Event> Events { get; set; } = default!;
    }
}
