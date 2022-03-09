using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MovieStoreRentalService.Data.Models;

public class Users : IdentityUser
{
    public Users()
    {
        UserRentals = new HashSet<UserRentals>();
    }

    [ForeignKey(nameof(Addresses))]
    public string AddressId { get; set; }
    public Addresses Addresses { get; set; }

    public ICollection<UserRentals> UserRentals { get; set; }
}