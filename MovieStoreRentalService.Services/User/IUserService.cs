using MovieStoreRentalService.DTO;

namespace MovieStoreRentalService.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsers();

        Task<ApplicationUser> GetUserById(string id);
    }
}
