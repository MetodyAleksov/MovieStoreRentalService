using Microsoft.EntityFrameworkCore;
using MovieStoreRentalService.Data.Common;

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
    }
}
