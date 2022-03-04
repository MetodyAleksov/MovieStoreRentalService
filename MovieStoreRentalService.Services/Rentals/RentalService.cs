using MovieStoreRentalService.Data.Common;
using MovieStoreRentalService.DTO;

namespace MovieStoreRentalService.Services.Rentals;

public class RentalService : IRentalService
{
    private readonly IRepository repo;

    public RentalService(IRepository repo)
    {
        this.repo = repo;
    }


    public (bool, string) ValidateModel(RentalDTO dto)
    {
        throw new NotImplementedException();
    }

    public (bool, string) AddRental(RentalDTO dto)
    {
        throw new NotImplementedException();
    }
}