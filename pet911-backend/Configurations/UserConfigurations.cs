using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using pet911_backend.Models;

namespace pet911_backend.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("Primary");

            builder.Property(x=>x.Id)
                .IsRequired()
                .HasColumnType("varchar(36)");

            builder.ToTable("User");

            builder.HasIndex(x => x.Id)
                .HasDatabaseName("FK_IndexIdUser");

            builder.Property(x => x.Name)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder
               .Property(x => x.PasswordSalt)
               .IsRequired()
               .HasColumnType("blob");

            builder
                .Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnType("blob");

            builder
               .Property(x => x.Birthdate)
               .IsRequired()
               .HasColumnType("date");

            builder
                .Property(x => x.IdRole)
                .IsRequired()
                .HasColumnType("varchar(36)");

            builder.HasOne(x => x.Role)
                .WithMany(x => x.users)
                .HasForeignKey(x => x.IdRole);
        }
    }
}
