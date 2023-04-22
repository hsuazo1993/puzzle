using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    public class ReservationMap : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                   .HasColumnName("Id")
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.Property(r => r.EventId)
                   .HasColumnName("EventId")
                   .IsRequired();

            builder.Property(r => r.ClientId)
                   .HasColumnName("ClientId")
                   .IsRequired();

            builder.HasOne(r => r.Event)
                   .WithMany(e => e.Reservations)
                   .HasForeignKey(r => r.EventId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Client)
                   .WithMany(c => c.Reservations)
                   .HasForeignKey(r => r.ClientId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.ReservationsFurnitures)
                   .WithOne(rf => rf.Reservation)
                   .HasForeignKey(rf => rf.ReservationId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
