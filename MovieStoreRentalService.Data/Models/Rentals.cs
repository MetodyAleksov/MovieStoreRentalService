using System.ComponentModel.DataAnnotations;

namespace MovieStoreRentalService.Data.Models;

public class Rentals
{
    public Rentals()
    {
        Id = Guid.NewGuid().ToString();
        UserRentals = new HashSet<UserRentals>();
    }

    [Required]
    [StringLength(70)]
    public string Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Type { get; set; }

    [Required]
    [StringLength(2083, MinimumLength = 10)]
    public string ImageUrl { get; set; }

    [Required]
    [Range(0, 1000)]
    public int AmountAvailable { get; set; }

    [Required]
    [Range(0, 100)]
    public decimal Price { get; set; }

    public ICollection<UserRentals> UserRentals { get; set; }
}