using DTOs;

namespace DataAccess.Models
{
    public class ClientStatus
    {
        public ClientStatusEnum Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<Client> Clients { get; set; } = default!;
    }
}
