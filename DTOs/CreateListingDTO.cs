using System.ComponentModel.DataAnnotations;

namespace InfiniteCreativity.DTO
{
    public class CreateListingDTO
    {
        public Guid ItemId { get; set; }
        [Range(1, double.MaxValue)]
        public double Price { get; set; }
    }
}
