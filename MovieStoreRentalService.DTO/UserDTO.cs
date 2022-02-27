namespace MovieStoreRentalService.DTO;

public class UserDTO
{
    public UserDTO()
    {
        Addresses = new HashSet<AddressDTO>();
        UserRentals = new HashSet<UserRentalDTO>();
    }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public ICollection<AddressDTO> Addresses { get; set; }

    public ICollection<UserRentalDTO> UserRentals { get; set; }
}