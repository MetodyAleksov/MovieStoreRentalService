using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using MovieStoreRentalService.Data.Models;

public class ApplicationUser : IdentityUser
{
    [ForeignKey(nameof(Addresses))]
    [PersonalData]
    public string? AddressId { get; set; }
    [PersonalData]
    public Addresses? Address { get; set; }
    
    public ICollection<UserRentals>? UserRentals { get; set; }
}