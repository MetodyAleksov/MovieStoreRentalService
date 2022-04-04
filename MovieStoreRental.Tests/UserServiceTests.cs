using Microsoft.Extensions.DependencyInjection;
using MovieStoreRental.Tests.Mocking;
using MovieStoreRentalService.Data.Common;
using MovieStoreRentalService.Services.Rentals;
using MovieStoreRentalService.Services.User;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MovieStoreRental.Tests
{
    public class UserServiceTests
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();

            var serviceCollection = new ServiceCollection();
            serviceProvider = serviceCollection
                .AddSingleton(service => dbContext.CreateContext())
                .AddSingleton<IRepository, Repository>()
                .AddSingleton<IRentalService, RentalService>()
                .AddSingleton<IUserService, UserService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public async Task GetAllUsersGetsAllUsers()
        {
            var service = serviceProvider.GetService<IUserService>();

            var users = await service.GetAllUsers();

            Assert.That(users.Count == 2);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IRepository repo)
        {
            await repo.AddAsync<ApplicationUser>(new ApplicationUser()
            {
                UserName = "Teddy"
            });
            await repo.AddAsync<ApplicationUser>(new ApplicationUser()
            {
                UserName = "Pesho"
            });

            await repo.SaveChangesAsync();
        }
    }
}