using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreRentalService.DTO.ViewModels
{
    public class UserEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
    }
}
