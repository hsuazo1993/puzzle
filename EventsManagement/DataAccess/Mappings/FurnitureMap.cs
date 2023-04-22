using DataAccess.Models;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mappings
{
    public class FurnitureMap : IEntityTypeConfiguration<Furniture>
    {
        public void Configure(EntityTypeBuilder<Furniture> builder)
        {
            builder.ToTable("Furnitures");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(f => f.Description)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(f => f.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(f => f.IsReserved)
                .IsRequired();

            builder.HasData(FurnitureSeed.Data);
        }
    }
}
