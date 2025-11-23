using Autorent.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Autorent.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {}

        public DbSet<User> Users => Set<User>();
        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Booking> Bookings => Set<Booking>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Car>().ToTable("Car");
            modelBuilder.Entity<Booking>().ToTable("Booking");

            base.OnModelCreating(modelBuilder);
        }
    }
}
