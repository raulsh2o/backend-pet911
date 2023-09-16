using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pet911_backend.Models;

namespace pet911_backend.Configurations
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id)
               .HasName("Primary");

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnType("varchar(36)");

            builder.ToTable("Role");

            builder.HasIndex(x => x.Id)
                .HasDatabaseName("FK_IndexIdRole");

            builder.Property(x => x.RoleType)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnType("TEXT");

        }

    }
}
