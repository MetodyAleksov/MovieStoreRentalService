using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieStoreRentalService.Data.Models;

namespace MovieStoreRentalService.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }

        public DbSet<Addresses> Addresses { get; set; }

        public DbSet<Rentals> Rentals { get; set; }

        public DbSet<UserRentals> UsersRentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString,
                b => b.MigrationsAssembly("MovieStoreRentalService"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRentals>()
                .HasKey(m => new { m.UserId, m.RentalId });

            base.OnModelCreating(modelBuilder);
        }
    }
}