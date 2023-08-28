using InfiniteCreativity.Models.CoreNS;
using System.Diagnostics.CodeAnalysis;

namespace InfiniteCreativity.Models.GameNS
{
    public class GameCharacter
    {
        public Guid Id { get; set; }
        public Character Character { get; set; }
        public int Order { get; set; }
        public GConnection Connection { get; set; }
    }
}
