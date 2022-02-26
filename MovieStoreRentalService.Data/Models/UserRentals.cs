using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreRentalService.Data.Models;

public class UserRentals
{
    [ForeignKey(nameof(Users))]
    public string UserId { get; set; }
    public Users Users { get; set; }

    [ForeignKey(nameof(Rentals))]
    public string RentalId { get; set; }
    public Rentals Rentals { get; set; }

    [Required]
    public DateTime DateRented { get; set; }

    [Required]
    public DateTime ReturnDate { get; set; }

    public bool IsPaid { get; set; }
}