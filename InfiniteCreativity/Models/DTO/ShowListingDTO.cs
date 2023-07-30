namespace InfiniteCreativity.Models.DTO
{
    public class ShowListingDTO
    {
        public int Id { get; set; }
        public ShowPlayerDTO Seller { get; set; }
        public ShowItemDTO Item { get; set; }
        public double Price { get; set; }
        public DateTime ListingDate { get; set; }
    }
}
