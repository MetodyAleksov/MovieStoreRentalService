﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using MovieStoreRentalService.Data.Models;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        UserRentals = new HashSet<UserRentals>();
        ShoppingCarts = new HashSet<ShoppingCarts>();
    }

    [ForeignKey(nameof(Addresses))]
    [PersonalData]
    public string? AddressId { get; set; }
    [PersonalData]
    public Addresses? Address { get; set; }
    
    public ICollection<UserRentals> UserRentals { get; set; }
    public ICollection<ShoppingCarts> ShoppingCarts { get; set; }
}