using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStoreRentalService.Data.Models
{
    public class ShoppingCarts
    {
        public ShoppingCarts()
        {
            Id = Guid.NewGuid().ToString();
            ShoppingCartsRentals = new HashSet<ShoppingCartsRentals>();
        }

        [Key]
        public string Id { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public bool IsActive { get; set; }

        public ICollection<ShoppingCartsRentals> ShoppingCartsRentals { get; set; }
    }
}
