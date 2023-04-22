using DataAccess.Models;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    public class ClientStatusMap : IEntityTypeConfiguration<ClientStatus>
    {
        public void Configure(EntityTypeBuilder<ClientStatus> builder)
        {
            builder.ToTable("ClientStatuses");

            builder.HasKey(cs => cs.Id);
            builder.Property(cs => cs.Id).ValueGeneratedNever();
            builder.Property(cs => cs.Name).IsRequired().HasMaxLength(50);
            builder.Property(cs => cs.Description).HasMaxLength(200);

            builder.HasMany(cs => cs.Clients)
                .WithOne(c => c.Status)
                .HasForeignKey(c => c.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(ClientStatusSeed.Data);
        }
    }
}
