using Entities;
using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Models.GameNS
{
    public class GConnection
    {
        public int Id { get; set; }
        public string ConnectionID { get; set; }
        public int Turn { get; set; } = 1;
        public ICollection<GameCharacter> Characters { get; set; }
        public MapDataObject Map { get; set; }
    }
}
