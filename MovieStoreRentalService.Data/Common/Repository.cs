using Microsoft.EntityFrameworkCore;

namespace MovieStoreRentalService.Data.Common
{
    public class Repository : IRepository
    {
        private readonly DbContext dbContext;

        public Repository(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            await dbContext.AddAsync(entity);
        }

        public IQueryable<T> All<T>() where T : class
        {
            return DbSet<T>().AsQueryable();
        }

        public void Remove(string id)
        {
            this.dbContext.Remove(id);
            this.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        private DbSet<T> DbSet<T>() where T : class 
        { 
            return dbContext.Set<T>(); 
        }


    }
}
