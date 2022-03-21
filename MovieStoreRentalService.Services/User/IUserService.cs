using MovieStoreRentalService.DTO.ViewModels;

namespace MovieStoreRentalService.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsers();

        Task<ApplicationUser> GetUserById(string id);

        Task<UserEditViewModel> GetUserForEdit(string id);

        Task<bool> UpdateUser(UserEditViewModel model);
    }
}
