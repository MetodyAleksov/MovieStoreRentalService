using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreRentalService.Data.Models
{
    public class ShoppingCartsRentals
    {
        [ForeignKey(nameof(ShoppingCarts))]
        public string ShoppingCartsId { get; set; }
        public ShoppingCarts ShoppingCarts { get; set; }

        [ForeignKey(nameof(Rentals))]
        public string RentalsId { get; set; }
        public Rentals Rentals { get; set; }
    }
}
