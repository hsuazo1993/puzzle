using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(c => c.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(100)")
                .IsRequired(); 

            builder.Property(e => e.DateOfBirth)
                .HasColumnName("DateOfBirth")
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnName("Email")
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(c => c.Phone)
                .HasColumnName("Phone")
                .HasColumnType("nvarchar(20)")
                .IsRequired();

            builder.Property(c => c.StatusId)
                .HasColumnName("StatusId")
                .HasColumnType("int")
                .IsRequired();

            builder.HasOne(c => c.Status)
                .WithMany(cs => cs.Clients)
                .HasForeignKey(c => c.StatusId)
                .IsRequired();

            builder.HasMany(c => c.Reservations)
                .WithOne(r => r.Client)
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
