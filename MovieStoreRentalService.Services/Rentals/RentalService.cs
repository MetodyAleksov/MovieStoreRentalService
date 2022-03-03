using MovieStoreRentalService.Data.Common;

namespace MovieStoreRentalService.Services.Rentals;

public class RentalService
{
    private readonly IRepository repo;

    public RentalService(IRepository repo)
    {
        this.repo = repo;
    }


}