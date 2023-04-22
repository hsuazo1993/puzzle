using DataAccess.Repositories;
using DTOs;

namespace BusinessLogic.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IClientService _clientService;
        public ReservationService(IReservationRepository reservationRepository, IClientService clientService)
        {
            _reservationRepository = reservationRepository;
            _clientService = clientService;
        }


        /// <summary>
        /// Each reservation must be assigned a type or category of event, it must be required when entering a new reservation or modifying it.
        /// </summary>
        /// <param name="eventTypeId"></param>
        /// <returns></returns>
        public async Task<bool> IsEventTypeValidAsync(int eventTypeId)
        {
            if (eventTypeId == default) return false;

            //NOTE: Here is missing to create the EventTypeRepository in order to check if eventTypeId exists in database.

            return true;
        }
        /// <summary>
        /// You cannot book more than one event at the same time, that means that reservations are only allowed during hours that are free(without reservations)
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<bool> IsTimeAvailable(TimeSpan startTime, TimeSpan endTime)
        {
            List<ReservationDto> reservationDtos = await _reservationRepository.GetAllValidReservationsAsync();

            var overlappingReservations = reservationDtos.Where(r => !(r.Event.EndTime <= startTime || r.Event.StartTime >= endTime));
            if (overlappingReservations.Any()) return false;


            return true;
        }
        /// <summary>
        /// You can only reserve from 7.30am to 9pm from Monday to Thursday.
        /// It can only be reserved from 3pm to 11pm from Friday to Saturday.
        /// Reservations are not allowed on Sunday.
        /// </summary>
        /// <param name="reservationDate"></param>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public bool IsValidReservationDate(DateTime reservationDate, TimeSpan startTime, TimeSpan endTime)
        {
            if (reservationDate.DayOfWeek == DayOfWeek.Sunday) return false;


            List<DayOfWeek> validDaysOfWeekScheduleOne = new List<DayOfWeek>()
            {
                DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday
            };


            bool reservationIsOnScheduleOne = validDaysOfWeekScheduleOne.Contains(reservationDate.DayOfWeek);
            TimeSpan validStartTime = reservationIsOnScheduleOne ? new TimeSpan(7, 30, 0) : new TimeSpan(15, 0, 0);
            TimeSpan validEndTime = reservationIsOnScheduleOne ? new TimeSpan(21, 0, 0) : new TimeSpan(23, 0, 0);


            if (startTime < validStartTime || startTime >= validEndTime) return false;

            if (endTime < validStartTime || endTime > validEndTime) return false;


            return true;
        }

        public bool IsClientOver21(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth > today.AddYears(-age))
            {
                age--;
            }
            return age >= 21;
        }
        /// <summary>
        /// For each reservation the client is required.
        /// It should not be allowed to create reservations for clients who are in status “DUE” or “CANCELLED”.
        /// Reservations are only allowed for clients over 21 years of age.
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<Result<string>> ValidateClientForReservationAsync(int clientId)
        {
            var getClientResult = await _clientService.GetByIdAsync(clientId);
            if (!getClientResult.IsSuccess)
            {
                return Result<string>.Failure(getClientResult.ErrorMessage);
            }

            var client = getClientResult.Data;
            if (client.StatusId == ClientStatusEnum.DUE || client.StatusId == ClientStatusEnum.CANCELED)
            {
                return Result<string>.Failure("Reservations are not allowed for clients in the selected status.");
            }

            if (!IsClientOver21(client.DateOfBirth))
            {
                return Result<string>.Failure("Reservations are only allowed for clients over 21 years of age.");
            }


            return Result<string>.Success();
        }

        /// <summary>
        /// Each reservation must be able to enter a detail of furniture.
        ///  It is only allowed to reserve furniture that is in an "available" state.
        ///  Only a maximum of 10 pieces of furniture are allowed to enter.
        /// </summary>
        /// <param name="furnituresDtos"></param>
        /// <returns></returns>
        public Result<string> ValidateFurnituresForReservation(List<FurnitureDto> furnituresDtos)
        {
            if (!furnituresDtos.Any()) return Result<string>.Success();


            if (furnituresDtos.Any(f => f.IsReserved))
            {
                return Result<string>.Failure("One or more selected furniture items are not available.");
            }
            if (furnituresDtos.Count > 10)
            {
                return Result<string>.Failure("Only a maximum of 10 furniture items are allowed per reservation.");
            }


            return Result<string>.Success();
        }


        public async Task<Result<ReservationDto>> CreateAsync(ReservationDto dto)
        {
            if (!await IsTimeAvailable(dto.Event.StartTime, dto.Event.EndTime))
            {
                return Result<ReservationDto>.Failure("The selected time is not available for reservations.");
            }

            if (!IsValidReservationDate(dto.Event.Date, dto.Event.StartTime, dto.Event.EndTime))
            {
                return Result<ReservationDto>.Failure("Reservations are not allowed on the selected date.");
            }


            var clientValidationResult = await ValidateClientForReservationAsync(dto.Client.Id);
            if (!clientValidationResult.IsSuccess)
            {
                return Result<ReservationDto>.Failure(clientValidationResult.ErrorMessage);
            }


            var furnituresValidationResult = ValidateFurnituresForReservation(dto.Furnitures);
            if (!furnituresValidationResult.IsSuccess)
            {
                return Result<ReservationDto>.Failure(furnituresValidationResult.ErrorMessage);
            }


            if(!await IsEventTypeValidAsync(dto.Event.Type.Id))
            {
                return Result<ReservationDto>.Failure("Invalid event type.");
            }


            dto = await _reservationRepository.SaveAsync(dto);


            return Result<ReservationDto>.Success(dto);
        }
    }
}
