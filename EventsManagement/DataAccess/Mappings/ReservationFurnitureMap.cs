using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    public class ReservationFurnitureMap : IEntityTypeConfiguration<ReservationFurniture>
    {
        public void Configure(EntityTypeBuilder<ReservationFurniture> builder)
        {
            builder.ToTable("ReservationFurniture");

            builder.HasKey(rf => new { rf.ReservationId, rf.FurnitureId });

            builder.HasOne(rf => rf.Reservation)
                .WithMany(r => r.ReservationsFurnitures)
                .HasForeignKey(rf => rf.ReservationId);

            builder.HasOne(rf => rf.Furniture)
                .WithMany(f => f.ReservationsFurnitures)
                .HasForeignKey(rf => rf.FurnitureId);
        }
    }
}
