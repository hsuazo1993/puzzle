using DTOs;

namespace DataAccess.Repositories
{
    public interface IClientRepository : 
        IGetData<ClientDto>, 
        ISaveData<ClientDto>, 
        IUpdateData<ClientDto>,
        IDeleteData
    {
        Task<ICollection<ClientDto>> GetClientsByStatusAsync(ClientStatusEnum clientStatus);
    }
}
