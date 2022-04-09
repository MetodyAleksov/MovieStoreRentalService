using Microsoft.AspNetCore.Identity;
using MovieStoreRentalService.Data.Models;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        ShoppingCarts = new HashSet<ShoppingCarts>();
    }

    public ICollection<ShoppingCarts> ShoppingCarts { get; set; }
}