using DataAccess.Models;
using DTOs;

namespace DataAccess.Utils
{
    public static class ClientStatusSeed
    {
        public static ICollection<ClientStatus> Data = new List<ClientStatus>()
        {
            new ClientStatus { Id = ClientStatusEnum.AVAILABLE, Name = "Available", Description = "Current client status is Available." },
            new ClientStatus { Id = ClientStatusEnum.DUE, Name = "Due", Description = "Current client status is Due." },
            new ClientStatus { Id = ClientStatusEnum.CANCELED, Name = "Canceled", Description = "Current client status is Canceled." }
        };
    }
}
