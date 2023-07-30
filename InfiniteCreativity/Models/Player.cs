using System.Reflection.PortableExecutable;

namespace InfiniteCreativity.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public double Purse { get; set; }
        public IEnumerable<Item>? Inventory { get; set; }
        public ICollection<Character> Characters { get; set; }
        public IEnumerable<Listing> Listing { get; set; }
    }
}
