using Microsoft.EntityFrameworkCore;
using MovieStoreRentalService.Data.Common;
using MovieStoreRentalService.DTO.ViewModels;

namespace MovieStoreRentalService.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRepository _repo;

        public UserService(IRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            return await _repo.All<ApplicationUser>().ToListAsync();
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await _repo.All<ApplicationUser>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserEditViewModel> GetUserForEdit(string id)
        {
            var user = await _repo.All<ApplicationUser>().FirstAsync(u => u.Id == id);

            return new UserEditViewModel()
            {
                Id = user.Id,
                Username = user.UserName
            };
        }

        public async Task<bool> UpdateUser(UserEditViewModel model)
        {
            bool result = false;
            var user = await _repo.All<ApplicationUser>().SingleAsync(u => u.Id == model.Id) ;

            if (user != null)
            {
                user.UserName = model.Username;

                await _repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }
    }
}
