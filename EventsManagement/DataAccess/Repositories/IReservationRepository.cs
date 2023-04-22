using DTOs;

namespace DataAccess.Repositories
{
    public interface IReservationRepository : ISaveData<ReservationDto>
    {
        Task<List<ReservationDto>> GetAllValidReservationsAsync();
    }
}
