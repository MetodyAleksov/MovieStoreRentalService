using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MovieStoreRentalService.Data.Models;

public class Users : IdentityUser
{
    [PersonalData]
    public bool IsAdmin { get; set; }

    [ForeignKey(nameof(Addresses))]
    [PersonalData]
    public string? AddressId { get; set; }
    [PersonalData]
    public Addresses? Addresses { get; set; }
    
    public ICollection<UserRentals>? UserRentals { get; set; }
}