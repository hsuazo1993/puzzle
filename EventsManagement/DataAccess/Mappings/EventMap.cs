using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    public class EventMap : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("Id").IsRequired();
            builder.Property(e => e.TypeId).HasColumnName("TypeId").IsRequired();
            builder.Property(e => e.Date).HasColumnName("Date").IsRequired();
            builder.Property(e => e.StartTime).HasColumnName("StartTime").IsRequired();
            builder.Property(e => e.EndTime).HasColumnName("EndTime").IsRequired();

            builder.HasOne(e => e.Type)
                .WithMany()
                .HasForeignKey(e => e.TypeId);

            builder.HasMany(e => e.Reservations)
                .WithOne(r => r.Event)
                .HasForeignKey(r => r.EventId);
        }
    }
}
