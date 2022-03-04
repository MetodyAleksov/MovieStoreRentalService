using MovieStoreRentalService.DTO;

namespace MovieStoreRentalService.Services.Rentals;

public interface IRentalService
{
    (bool, string) ValidateModel(RentalDTO dto);
    (bool, string) AddRental(RentalDTO dto);
}