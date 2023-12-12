using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using pet911_backend.Models;

namespace pet911_backend.Configurations
{
    public class AdoptConfigurations
    {
        public void Configure(EntityTypeBuilder<Adopt> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("Primary");

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnType("varchar(36)");

            builder.ToTable("adopt");

            builder.HasIndex(x => x.Id)
                .HasDatabaseName("FK_IndexIdPet");

            builder.Property(x => x.Name)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.Property(x => x.Age)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.Sex)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.Race)
                .HasColumnType("varchar(100)");

            builder.Property(x => x.Allergies)
                .HasColumnType("TEXT");

            builder.Property(x => x.Notes)
                .HasColumnType("TEXT");

            builder.Property(x => x.Image)
                .HasColumnType("TEXT");

            builder.Property(x => x.Type)
                .HasColumnType("varchar(100)");

        }
    }
}
