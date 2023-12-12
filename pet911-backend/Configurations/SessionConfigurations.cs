using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using pet911_backend.Models;
using pet911_backend.Models.Dto;

namespace pet911_backend.Configurations
{
    public class SessionConfigurations : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(x => x.Id)
               .HasName("Primary");

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnType("varchar(36)");

            builder.ToTable("session");

            builder.HasIndex(x => x.Id)
                .HasDatabaseName("FK_IndexIdSession");

            builder.Property(x => x.Email)
               .IsRequired()
               .HasColumnType("varchar(100)");


        }

    }
}

