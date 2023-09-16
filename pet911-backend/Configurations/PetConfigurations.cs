using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using pet911_backend.Models;

namespace pet911_backend.Configurations
{
    public class PetConfigurations : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("Primary");

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnType("varchar(36)");

            builder.ToTable("Pet");

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

            builder
                .Property(x => x.IdUser)
                .IsRequired()
                .HasColumnType("varchar(36)");

            builder.HasOne(x => x.user)
                .WithMany(x => x.pets)
                .HasForeignKey(x => x.IdUser);
        }
    }
}
