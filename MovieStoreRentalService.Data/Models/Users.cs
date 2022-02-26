using System.ComponentModel.DataAnnotations;

namespace MovieStoreRentalService.Data.Models;

public class Users
{
    public Users()
    {
        Id = Guid.NewGuid().ToString();
        Addresses = new HashSet<Addresses>();
        UserRentals = new HashSet<UserRentals>();
    }

    [Required]
    [StringLength(70)]
    public string Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Username { get; set; }

    [Required]
    [StringLength(100)]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; }

    [StringLength(30)]
    public string? PhoneNumber { get; set; }

    public ICollection<Addresses> Addresses { get; set; }

    public ICollection<UserRentals> UserRentals { get; set; }
}