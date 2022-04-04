using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MovieStoreRentalService.Data;

namespace MovieStoreRental.Tests.Mocking
{
    internal class InMemoryDbContext
    {
        private readonly SqliteConnection connection;
        private readonly DbContextOptions<ApplicationDbContext> dbContextOptions;

        public InMemoryDbContext(DbContextOptions<ApplicationDbContext> dbContext)
        {
            this.connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            using var context = new ApplicationDbContext(dbContext);
            context.Database.EnsureCreated();
        }

        public ApplicationDbContext CreateContext()
            => new ApplicationDbContext(dbContextOptions);

        public void Dispose() => connection.Dispose();
    }
}
