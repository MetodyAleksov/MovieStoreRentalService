using MovieStoreRentalService.DTO;

namespace MovieStoreRentalService.Services.Rentals;

public interface IRentalService
{
    Task<(bool, string)> AddRental(RentalDTO dto);

    IEnumerable<RentalDTO> ListAllRentals();

    (bool, RentalDTO) FindById(string id);

    void RemoveRental(string id);

    Task EditRental(string id, RentalDTO newData);
}