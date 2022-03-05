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


    private (bool, string) ValidateModel(RentalDTO dto)
    {
        throw new NotImplementedException();
    }

    public (bool, string) AddRental(RentalDTO dto)
    {
        Data.Models.Rentals rental = new Data.Models.Rentals()
        {
            ImageUrl = dto.ImageURL,
            Name = dto.Name,
            AmountAvailable = dto.AmountAvailable,
            Price = dto.Price,
            Type = dto.RentalType.ToString()
        };

        repo.Add(rental);
        repo.SaveChanges();

        return (true, null);
    }
}