using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MovieStoreRentalService.Data.Models;

public class Users : IdentityUser
{
    public Users()
    {
        Addresses = new HashSet<Addresses>();
        UserRentals = new HashSet<UserRentals>();
    }

    public ICollection<Addresses> Addresses { get; set; }

    public ICollection<UserRentals> UserRentals { get; set; }
}