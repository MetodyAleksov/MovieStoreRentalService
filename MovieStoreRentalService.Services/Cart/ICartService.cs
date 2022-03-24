using MovieStoreRentalService.DTO;

namespace MovieStoreRentalService.Services
{
    public interface ICartService
    {
        Task<CartDTO> GetUsersCart(string userId);
        Task AddCart(string userId);
        Task AddRentalToCart(string rentalId, string userId, string cartId);
    }
}
