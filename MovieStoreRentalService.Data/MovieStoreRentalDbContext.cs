using Microsoft.EntityFrameworkCore;

namespace MovieStoreRentalService.Data
{
    public class MovieStoreRentalDbContext : DbContext
    {
        public MovieStoreRentalDbContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}