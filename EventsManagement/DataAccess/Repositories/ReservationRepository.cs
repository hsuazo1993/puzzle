using DataAccess.Models;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly EventsManagementDbContext _eventsManagementDbContext;
        public ReservationRepository(EventsManagementDbContext eventsManagementDbContext)
        {
            _eventsManagementDbContext = eventsManagementDbContext;
        }


        public async Task<List<ReservationDto>> GetAllValidReservationsAsync()
        {
            //NOTE: Due to time constraints I just mapped essential properties, otherwise would have mapped using Mapster custom mapping.
            return await _eventsManagementDbContext.Reservations.AsQueryable()
                                                                .AsNoTracking()
                                                                .Where(reservation => reservation.Event.Date >= DateTime.Now.Date)
                                                                //.ProjectToType<ReservationDto>()
                                                                .Select(reservation => new ReservationDto()
                                                                {
                                                                    Id = reservation.Id,
                                                                    Client = new ClientDto()
                                                                    {
                                                                        Id = reservation.ClientId,
                                                                    },
                                                                    Event = new EventDto()
                                                                    {
                                                                        Id = reservation.Event.TypeId,
                                                                        Date = reservation.Event.Date,
                                                                        StartTime = reservation.Event.StartTime,
                                                                        EndTime = reservation.Event.EndTime
                                                                    },
                                                                    Furnitures = (from rf in reservation.ReservationsFurnitures
                                                                                  select new FurnitureDto()
                                                                                  {
                                                                                      Id = rf.FurnitureId
                                                                                  }).ToList()
                                                                }).ToListAsync();
        }

        public async Task<ReservationDto> SaveAsync(ReservationDto dto)
        {
            //NOTE: Due to time constraints I just mapped essential properties, otherwise would have mapped using Mapster custom mapping.
            var reservation = new Reservation()
            {
                ClientId = dto.Client.Id,
                Event = new Event()
                {
                    TypeId = dto.Event.Type.Id,
                    Date = dto.Event.Date,
                    StartTime = dto.Event.StartTime,
                    EndTime = dto.Event.EndTime
                },
                ReservationsFurnitures = (from rf in dto.Furnitures
                                          select new ReservationFurniture()
                                          {
                                              FurnitureId = rf.Id,
                                          }).ToList()
            };
            await _eventsManagementDbContext.Reservations.AddAsync(reservation);
            await _eventsManagementDbContext.SaveChangesAsync();


            //NOTE: Due to time constraints I just mapped essential properties, otherwise would have mapped using Mapster custom mapping.
            return new ReservationDto()
            {
                Id = reservation.Id,
                Client = new ClientDto()
                {
                    Id = reservation.ClientId,
                },
                Event = new EventDto()
                {
                    Id = reservation.Event.TypeId,
                    Date = reservation.Event.Date,
                    StartTime = reservation.Event.StartTime,
                    EndTime = reservation.Event.EndTime
                },
                Furnitures = (from rf in reservation.ReservationsFurnitures
                              select new FurnitureDto()
                              {
                                  Id = rf.FurnitureId
                              }).ToList()
            };
        }
    }
}
