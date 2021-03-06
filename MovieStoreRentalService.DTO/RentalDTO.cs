using MovieStoreRentalService.DTO.Common.Enums;

namespace MovieStoreRentalService.DTO;

public class RentalDTO
{
    public RentalDTO()
    {
    }

    public string Id { get; set; }

    public string Name { get; set; }

    public string ImageURL { get; set; }

    public RentalType RentalType { get; set; }

    public int AmountAvailable { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }

    public DateTime TimeAdded { get; set; }

    public string DirectorName { get; set; }
}