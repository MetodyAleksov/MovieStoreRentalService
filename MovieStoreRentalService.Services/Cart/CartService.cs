using MovieStoreRentalService.Data.Common;
using MovieStoreRentalService.Data.Models;
using MovieStoreRentalService.DTO;

namespace MovieStoreRentalService.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly IRepository _repository;

        public CartService(IRepository repo)
        {
            _repository = repo;
        }

        public async Task AddCart(string userId)
        {
            bool hasCart = _repository.All<ShoppingCarts>().Any(c => c.IsActive);
            if (hasCart)
            {
                return;
            }

            ShoppingCarts cart = new ShoppingCarts()
            {
                ApplicationUserId = userId,
                IsActive = true
            };

            await _repository.AddAsync(cart);
            await _repository.SaveChangesAsync();
        }

        public async Task AddRentalToCart(string rentalId, string userId = null ,string cartId = null)
        {
            if (userId == null && cartId == null)
            {
                throw new InvalidOperationException("Both parameters cannot be null");
            }

            ShoppingCarts cart = null;
            bool rentalIsInvalid = !_repository.All<Data.Models.Rentals>().Any(r => r.Id == rentalId);

            if (rentalIsInvalid)
            {
                throw new ArgumentException("Rental id is invalid!");
            }

            if (userId != null)
            {
                cart = _repository.All<ShoppingCarts>().SingleOrDefault(sc => sc.ApplicationUserId == userId);

                if(cart == null)
                {
                    throw new ArgumentException("User does not have a valid cart!");
                }
            }
            else if (cartId != null)
            {
                cart = _repository.All<ShoppingCarts>().FirstOrDefault(s => s.Id == cartId);

                if (cart == null)
                {
                    throw new ArgumentException("CartId is invalid!");
                }
            }
            
            cart.ShoppingCartsRentals.Add(new ShoppingCartsRentals()
            {
                ShoppingCartsId = cart.Id,
                RentalsId = rentalId
            });

            await _repository.SaveChangesAsync();
        }

        public async Task<CartDTO> GetUsersCart(string userId)
        {
            var cart = await _repository.GetShoppingCarts(userId);

            if (cart == null)
            {
                throw new ArgumentException("User does not have a cart!");
            }

            return new CartDTO()
            {
                UserId = userId,
                CartId = cart.Id,
                Rentals = cart.ShoppingCartsRentals
                .Select(scr => scr.Rentals)
                .Select(sr => new RentalDTO()
                {
                    Id = sr.Id,
                    AmountAvailable = sr.AmountAvailable,
                    Name = sr.Name,
                    Description = sr.Description,
                    ImageURL = sr.ImageUrl,
                    Price = sr.Price,
                    TimeAdded = sr.TimeAdded
                })
                .ToHashSet(),
            };
        }
    }
}
