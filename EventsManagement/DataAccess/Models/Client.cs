using DTOs;

namespace DataAccess.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public ClientStatusEnum StatusId { get; set; }
        public ClientStatus Status { get; set; } = default!;
        public virtual ICollection<Reservation> Reservations { get; set; } = default!;
    }
}
