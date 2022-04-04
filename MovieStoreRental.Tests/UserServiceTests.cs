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
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}