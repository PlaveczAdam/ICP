namespace InfiniteCreativity.Models.CoreNS
{
    public class Listing
    {
        public int Id { get; set; }
        public Player Seller { get; set; }
        public Item Item { get; set; }
        public double Price { get; set; }
        public DateTime ListingDate { get; set; }

    }
}
