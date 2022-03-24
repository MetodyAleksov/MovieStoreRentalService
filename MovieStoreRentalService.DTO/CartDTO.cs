namespace MovieStoreRentalService.DTO
{
    public class CartDTO
    {
        public string UserId { get; set; }

        public string CartId { get; set; }

        public ICollection<RentalDTO> Rentals { get; set; }
    }
}
