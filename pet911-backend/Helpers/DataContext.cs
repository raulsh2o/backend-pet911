using Microsoft.EntityFrameworkCore;
using pet911_backend.Configurations;
using pet911_backend.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Diagnostics.Eventing.Reader;
using pet911_backend.Models.Dto;

namespace pet911_backend.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }


        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Pet> Pet { get; set; }

        public DbSet<Notification> Notification { get; set; }
        public DbSet<Service> Service { get; set; }

        public DbSet<Session> Session { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfigurations());
            modelBuilder.ApplyConfiguration(new PetConfigurations());
            modelBuilder.ApplyConfiguration(new RoleConfigurations());
            modelBuilder.ApplyConfiguration(new ServiceConfigurations());
            modelBuilder.ApplyConfiguration(new SessionConfigurations());

        }
    }
}
