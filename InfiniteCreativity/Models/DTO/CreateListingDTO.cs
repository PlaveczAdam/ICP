using System.ComponentModel.DataAnnotations;

namespace InfiniteCreativity.Models.DTO
{
    public class CreateListingDTO
    {
        public int ItemId { get; set; }
        [Range(1,double.MaxValue)]
        public double Price { get; set; }
    }
}
