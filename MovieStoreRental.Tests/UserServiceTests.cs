using Microsoft.Extensions.DependencyInjection;
using MovieStoreRental.Tests.Mocking;
using MovieStoreRentalService.Data.Common;
using MovieStoreRentalService.Services.Rentals;
using MovieStoreRentalService.Services.User;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MovieStoreRental.Tests
{
    public class OrderServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IRepository, Repository>()
                .AddSingleton<IRentalService, RentalService>()
                .AddSingleton<IUserService, UserService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public async Task TestGetAllUsers()
        {
            var service = serviceProvider.GetService<IUserService>();
            var users = await service.GetAllUsers();

            Assert.AreEqual(2, users.Count);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IRepository repo)
        {
            await repo.AddAsync(new ApplicationUser()
            {
                UserName = "Teddy"
            });
            await repo.AddAsync(new ApplicationUser()
            {
                UserName = "Pesho"
            });
            await repo.SaveChangesAsync();
        }
    }
}