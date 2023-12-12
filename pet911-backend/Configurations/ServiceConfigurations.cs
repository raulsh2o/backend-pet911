using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using pet911_backend.Models;

namespace pet911_backend.Configurations
{
    public class ServiceConfigurations : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => x.Id)
               .HasName("Primary");

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnType("varchar(36)");

            builder.ToTable("service");

            builder.HasIndex(x => x.Id)
                .HasDatabaseName("FK_IndexIdService");

            builder.Property(x => x.Type)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("TEXT");

            builder.Property(x => x.OpeningTime)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(x => x.ClosingTime)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(x => x.ContactNumber)
                .IsRequired()
                .HasColumnType("varchar(25)");

            builder.Property(x => x.Latitude)
                .IsRequired()
                .HasColumnType("double");

            builder.Property(x => x.Longitude)
                .IsRequired()
                .HasColumnType("double");

            builder.Property(x => x.Catalogue)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(e => e.Sponsored)
              .IsRequired()
              .HasColumnType("tinyint(1)"); ;

            builder.Property(x => x.IdUser)
                .IsRequired()
                .HasColumnType("varchar(36)");

            builder.HasOne(x => x.user)
                .WithMany(x => x.services)
                .HasForeignKey(x => x.IdUser);

        }

    }
}
