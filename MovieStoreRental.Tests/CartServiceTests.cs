using Microsoft.Extensions.DependencyInjection;
using MovieStoreRental.Tests.Mocking;
using MovieStoreRentalService.Data.Common;
using MovieStoreRentalService.Data.Models;
using MovieStoreRentalService.DTO;
using MovieStoreRentalService.Services;
using MovieStoreRentalService.Services.Cart;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreRental.Tests
{
    internal class CartServiceTests
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
                .AddSingleton<ICartService, CartService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IRepository>();
            await SeedDbAsync(repo);
        }

        [Test]
        public async Task GetUsersCartTest()
        {
            var repo = serviceProvider.GetService<IRepository>();
            var service = serviceProvider.GetService<ICartService>();
            string userId = repo.All<ApplicationUser>().First().Id;

            ShoppingCarts cart = repo.All<ShoppingCarts>().FirstOrDefault(s => s.ApplicationUserId == userId);

            CartDTO cartDTO = new CartDTO()
            {
                CartId = cart.Id,
                UserId = userId
            };

            var usersCart = await service.GetUsersCart(userId);

            Assert.That(cartDTO.UserId == usersCart.UserId && cartDTO.CartId == usersCart.CartId);
        }

        [Test]
        public async Task AddCartTestDoesntAddCartToUserWithOne()
        {
            var repo = serviceProvider.GetService<IRepository>();
            var service = serviceProvider.GetService<ICartService>();

            int initualCount = repo.All<ShoppingCarts>().Count();

            await service.AddCart(repo.All<ApplicationUser>().Single(u => u.UserName == "Pesho").Id);

            Assert.That(initualCount == repo.All<ShoppingCarts>().Count());
        }

        [Test]
        public async Task AddRentalToCartTests()
        {
            var repo = serviceProvider.GetService<IRepository>();
            var service = serviceProvider.GetService<ICartService>();

            await repo.AddAsync(new ApplicationUser()
            {
                UserName = "Go6o"
            });
            await repo.SaveChangesAsync();

            Assert.CatchAsync<InvalidOperationException>(async () =>
            {
               await service.AddRentalToCart("", null, null);
            });
            Assert.CatchAsync<ArgumentException>(async () =>
            {
                await service.AddRentalToCart("",
                    repo.All<ApplicationUser>().First(u => u.UserName == "Go6o").Id, "");
            });

            await repo.AddAsync(new Rentals()
            {
                Name = "Something",
                AmountAvailable = 10,
                Description = "smt",
                ImageUrl = "smt",
                Price = 10,
                TimeAdded = DateTime.Now,
                Type = "Movie"
            });
            await service.AddCart(repo.All<ApplicationUser>().Single(u => u.UserName == "Go6o").Id);
            await repo.SaveChangesAsync();

            await service.AddRentalToCart(repo.All<Rentals>().First().Id, repo.All<ApplicationUser>().First().Id, null);

            Assert.That(repo.All<ShoppingCartsRentals>().Count() > 0);
        }

        [Test]
        public async Task RemoveRentalTask()
        {
            var repo = serviceProvider.GetService<IRepository>();
            var service = serviceProvider.GetService<ICartService>();

            await repo.AddAsync(new Rentals()
            {
                Name = "Something",
                AmountAvailable = 10,
                Description = "smt",
                ImageUrl = "smt",
                Price = 10,
                TimeAdded = DateTime.Now,
                Type = "Movie"
            });

            await repo.SaveChangesAsync();

            int initialCount = repo.All<ShoppingCartsRentals>().Count();

            Assert.CatchAsync<InvalidOperationException>(async () =>
            {
                await service.RemoveRentalFromCart(repo.All<Rentals>().First().Id, repo.All<ApplicationUser>().Single(u => u.UserName == "Pesho").Id);
            });

            await service.AddCart(repo.All<ApplicationUser>().Single(u => u.UserName == "Go6o").Id);

            Assert.CatchAsync<InvalidOperationException>(async () =>
            {
                await service.RemoveRentalFromCart(repo.All<Rentals>().First().Id, repo.All<ApplicationUser>().Single(u => u.UserName == "Pesho").Id);
            });

            await service.AddRentalToCart(repo.All<Rentals>().First().Id, repo.All<ApplicationUser>().Single(u => u.UserName == "Teddy").Id, "");
            await service.RemoveRentalFromCart(repo.All<Rentals>().First().Id, repo.All<ApplicationUser>().Single(u => u.UserName == "Teddy").Id);

            Assert.That(repo.All<ShoppingCartsRentals>().Count() == initialCount - 1);
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

            await repo.AddAsync(new ShoppingCarts()
            {
                ApplicationUser = repo.All<ApplicationUser>().First(),
                ApplicationUserId = repo.All<ApplicationUser>().First().Id,
                IsActive = true
            }) ;

            await repo.SaveChangesAsync();
        }
    }
}
