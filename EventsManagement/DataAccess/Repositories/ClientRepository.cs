using DataAccess.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace DataAccess.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly EventsManagementDbContext _eventsManagementDbContext;
        public ClientRepository(EventsManagementDbContext eventsManagementDbContext)
        {
            _eventsManagementDbContext = eventsManagementDbContext;
        }


        public async Task<ICollection<ClientDto>> GetAllAsync()
        {
            return await _eventsManagementDbContext.Clients.AsQueryable()
                                                           .AsNoTracking()
                                                           .ProjectToType<ClientDto>()
                                                           .ToListAsync();
        }

        public async Task<ICollection<ClientDto>> GetClientsByStatusAsync(ClientStatusEnum clientStatus)
        {
            return await _eventsManagementDbContext.Clients.AsQueryable()
                                               .AsNoTracking()
                                               .Where(client => client.StatusId == clientStatus)
                                               .ProjectToType<ClientDto>()
                                               .ToListAsync();
        }

        public async Task<ClientDto> GetByIdAsync(int id)
        {
            return await _eventsManagementDbContext.Clients.AsQueryable()
                                                           .AsNoTracking()
                                                           .Where(e => e.Id == id)
                                                           .ProjectToType<ClientDto>()
                                                           .FirstAsync();
        }

        public async Task<ClientDto> SaveAsync(ClientDto dto)
        {
            var client = dto.Adapt<Client>();
            await _eventsManagementDbContext.Clients.AddAsync(client);
            await _eventsManagementDbContext.SaveChangesAsync();


            return client.Adapt<ClientDto>();
        }

        public async Task<bool> UpdateAsync(int id, ClientDto dto)
        {
            Client? client = await _eventsManagementDbContext.Clients.AsQueryable()
                                                                     .FirstOrDefaultAsync(e => e.Id == id);

            if (client == null) return false;


            client.Name = dto.Name;
            client.DateOfBirth = dto.DateOfBirth;
            client.Email = dto.Email;
            client.Phone = dto.Phone;
            client.StatusId = dto.StatusId;

            await _eventsManagementDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Client client = await _eventsManagementDbContext.Clients.AsQueryable()
                                                                    .FirstAsync(e => e.Id == id);

            if (client == null) return false;


            _eventsManagementDbContext.Clients.Remove(client);
            await _eventsManagementDbContext.SaveChangesAsync();


            return true;
        }
    }
}
