using DataAccess.Models;

namespace DataAccess.Utils
{
    public static class EventTypeSeed
    {
        public static ICollection<EventType> Data = new List<EventType>()
        {
            new EventType
            {
                Id = 1,
                Name = "Birthday",
                Description = "Birthday event type description."
            },
            new EventType
            {
                Id = 2,
                Name = "Wedding",
                Description = "Wedding event type description."
            },
            new EventType
            {
                Id = 3,
                Name = "Conference",
                Description = "Conference event type description."
            },
            new EventType
            {
                Id = 4,
                Name = "Training",
                Description = "Training event type description."
            }
        };
    }
}
