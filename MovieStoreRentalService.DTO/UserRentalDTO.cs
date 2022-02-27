namespace MovieStoreRentalService.DTO;

public class UserRentalDTO
{
    public UserDTO User { get; set; }

    public RentalDTO Rental { get; set; }

    public DateTime DateRented { get; set; }

    public DateTime ReturnDate { get; set; }

    public bool IsPaid { get; set; }
}