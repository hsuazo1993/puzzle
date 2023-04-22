using DTOs;

namespace BusinessLogic.Services
{
    public interface IReservationService
    {
        Task<Result<ReservationDto>> CreateAsync(ReservationDto dto);
    }
}
