using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreRentalService.Data.Models;

public class Addresses
{
    public Addresses()
    {
        Id = Guid.NewGuid().ToString();
    }

    [Required]
    [StringLength(70)]
    public string Id { get; set; }

    [ForeignKey(nameof(Users))]
    public string UserId { get; set; }

    public Users Users { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string StreetName { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string City { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Country { get; set; }
}