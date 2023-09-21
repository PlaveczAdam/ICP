using System.ComponentModel.DataAnnotations.Schema;
using InfiniteCreativity.Models.GameNS;

namespace InfiniteCreativity.Models.CoreNS
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public double Money { get; set; }
        public int CharacterSlot { get; set; }
        public int QuestSlot { get; set; }

        public ICollection<Item>? Inventory { get; set; }
        public ICollection<Character> Characters { get; set; }

        public ICollection<Listing> Listing { get; set; }

        public ICollection<FeConnection> FeConnections { get; set; }
        public ICollection<GConnection> GConnections { get; set; }
        [InverseProperty("Recipient")]
        public ICollection<Message> RecievedMessages { get; set; }
        [InverseProperty("Sender")]
        public ICollection<Message> SentMessages { get; set; }
    }
}
