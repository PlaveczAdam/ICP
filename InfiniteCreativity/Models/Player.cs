using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace InfiniteCreativity.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public double Money { get; set; }
        public IEnumerable<Item>? Inventory { get; set; }
        public ICollection<Character> Characters { get; set; }
        public IEnumerable<Listing> Listing { get; set; }
        public ICollection<Connection> Connections { get; set; }
        [InverseProperty("Recipient")]
        public ICollection<Message> RecievedMessages { get; set; }
        [InverseProperty("Sender")]
        public ICollection<Message> SentMessages { get; set; }
    }
}
