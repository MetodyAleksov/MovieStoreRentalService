using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreRentalService.Data.Models;

public class Rentals
{
    public Rentals()
    {
        Id = Guid.NewGuid().ToString();
        ShoppingCartsRentals = new HashSet<ShoppingCartsRentals>();
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

    [StringLength(300)]
    public string Description { get; set; }

    public DateTime TimeAdded { get; set; }
    public ICollection<ShoppingCartsRentals> ShoppingCartsRentals { get; set; }

    [ForeignKey(nameof(MovieDirector))]
    public string MovieDirectorId { get; set; }
    public MovieDirector MovieDirector { get; set; }
}