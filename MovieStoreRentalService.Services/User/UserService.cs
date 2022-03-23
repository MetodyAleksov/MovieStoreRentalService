using Microsoft.EntityFrameworkCore;
using MovieStoreRentalService.Data.Common;
using MovieStoreRentalService.DTO;
using MovieStoreRentalService.Services.Rentals;

namespace MovieStoreRentalService.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRentalService _rentalService;
        private readonly IRepository _repo;

        public UserService(IRepository repo, IRentalService rentalService)
        {
            _rentalService = rentalService;
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

        public async Task<IEnumerable<RentalDTO>> GetUsersRentals(string id)
        {
            ApplicationUser user = await GetUserById(id);

            List<RentalDTO> rentals = new List<RentalDTO>();

            foreach (var rental in user.UserRentals)
            {
                (bool exists, RentalDTO rentalDTO) = _rentalService.FindById(rental.RentalId);

                if (exists)
                {
                    rentals.Add(rentalDTO);
                }
            }

            return rentals;
        }
    }
}
