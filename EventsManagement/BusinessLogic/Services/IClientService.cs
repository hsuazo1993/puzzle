using DTOs;

namespace BusinessLogic.Services
{
    public interface IClientService
    {
        Task<Result<List<ClientDto>>> GetAllAsync();
        Task<Result<ClientDto>> GetByIdAsync(int id);
        Task<Result<ClientDto>> CreateAsync(ClientDto clientDto);
        Task<Result<ClientDto>> UpdateAsync(ClientDto clientDto);
        Task<Result<ClientDto>> DeleteAsync(int id);
        Task<Result<List<ClientDto>>> GetClientsByStatusAsync(ClientStatusEnum status);
    }
}
