using DataAccess.Mappings;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class EventsManagementDbContext : DbContext
    {
        public EventsManagementDbContext(DbContextOptions<EventsManagementDbContext> options) : base(options)
        {
        }


        public DbSet<Reservation> Reservations { get; set; } = default!;
        public DbSet<Event> Events { get; set; } = default!;
        public DbSet<EventType> EventTypes { get; set; } = default!;
        public DbSet<Furniture> Furnitures { get; set; } = default!;
        public DbSet<ReservationFurniture> ReservationsFurnitures { get; set; } = default!;
        public DbSet<Client> Clients { get; set; } = default!;
        public DbSet<ClientStatus> ClientStatuses { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ReservationMap());
            modelBuilder.ApplyConfiguration(new EventMap());
            modelBuilder.ApplyConfiguration(new EventTypeMap());
            modelBuilder.ApplyConfiguration(new FurnitureMap());
            modelBuilder.ApplyConfiguration(new ReservationFurnitureMap());
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new ClientStatusMap());
        }
    }
}
