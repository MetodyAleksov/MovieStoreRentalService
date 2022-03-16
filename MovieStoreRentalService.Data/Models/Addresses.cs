using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreRentalService.Data.Models;

public class Addresses
{
    public Addresses()
    {
        Id = Guid.NewGuid().ToString();
        Users = new HashSet<ApplicationUser>();
    }

    [Required]
    [StringLength(70)]
    public string Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string StreetName { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string City { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Country { get; set; }

    public ICollection<ApplicationUser> Users { get; set; }
}