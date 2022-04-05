using Microsoft.Extensions.DependencyInjection;
using MovieStoreRental.Tests.Mocking;
using MovieStoreRentalService.Data.Common;
using MovieStoreRentalService.Data.Models;
using MovieStoreRentalService.DTO;
using MovieStoreRentalService.Services.Rentals;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreRental.Tests
{
    internal class RentalServiceTests
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
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public async Task AddRentalTest()
        {
            var service = serviceProvider.GetService<IRentalService>();
            var repo = serviceProvider.GetService<IRepository>();

            (bool isValid, string error) = await service.AddRental(new RentalDTO()
            {
                AmountAvailable = 10,
                Description = "Something interesting",
                ImageURL = "something possibly valid",
                Name = "Pesho",
                Price = (decimal)10,
                RentalType = MovieStoreRentalService.DTO.Common.Enums.RentalType.Movie,
                TimeAdded = DateTime.UtcNow
            });

            Assert.That(isValid, Is.True);
            Assert.That(error, Is.Null);
            Assert.That(repo.All<Rentals>().Count() == 1);
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
