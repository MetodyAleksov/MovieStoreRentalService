using MovieStoreRentalService.DTO;

namespace MovieStoreRentalService.Services.User
{
    public interface IUserService
    {
        Task<ICollection<ApplicationUser>> GetAllUsers();

        Task<ApplicationUser> GetUserById(string id);
    }
}
