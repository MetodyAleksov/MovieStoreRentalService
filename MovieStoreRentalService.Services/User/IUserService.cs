namespace MovieStoreRentalService.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
    }
}
