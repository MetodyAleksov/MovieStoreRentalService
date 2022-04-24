using System.ComponentModel.DataAnnotations;

namespace MovieStoreRentalService.Data.Models
{
    public class MovieDirector
    {
        public MovieDirector()
        {
            Id = new Guid().ToString();
            Movies = new HashSet<Rentals>();
        }

        [Key]
        public string Id { get; set; }

        [MinLength(2)]
        [MaxLength(255)]
        public string Name { get; set; }

        public ICollection<Rentals> Movies { get; set; }
    }
}
