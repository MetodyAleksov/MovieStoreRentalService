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

            Assert.That(isValid, Is.True, "Returns false state");
            Assert.That(error, Is.Null);
            Assert.That(repo.All<Rentals>().Count() == 2, "Service doesn't add the object");
        }

        [Test]
        public async Task ListAllRentalsTest()
        {
            var service = serviceProvider.GetService<IRentalService>();
            var repo = serviceProvider.GetService<IRepository>();

            var repoData = repo.All<Rentals>().ToList();
            var serviceData = service.ListAllRentals().ToList();

            Assert.That(repoData.Count == serviceData.Count);
        }

        [Test]
        public async Task FindRentalByIdTest()
        {
            var service = serviceProvider.GetService<IRentalService>();
            var repo = serviceProvider.GetService<IRepository>();

            string rentalId = repo.All<Rentals>().First().Id;

            (bool isValid, RentalDTO dto) = service.FindById(rentalId);

            Assert.That(isValid, Is.True);
        }

        [Test]
        public async Task RemoveRentalTest()
        {
            var service = serviceProvider.GetService<IRentalService>();
            var repo = serviceProvider.GetService<IRepository>();
            int initialCount = repo.All<Rentals>().Count();

            service.RemoveRental(repo.All<Rentals>().First().Id);

            Assert.That(service.ListAllRentals().Count() == (initialCount - 1));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IRepository repo)
        {
            await repo.AddAsync<Rentals>(new Rentals()
            {
                Name = "Peter",
                Type = "Movie",
                ImageUrl = "Valid",
                Description = "Possible"
            });

            await repo.SaveChangesAsync();
        }
    }
}
