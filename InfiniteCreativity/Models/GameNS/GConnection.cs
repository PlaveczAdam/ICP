using Entities;
using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Models.GameNS.Enemys;
using Microsoft.EntityFrameworkCore;

namespace InfiniteCreativity.Models.GameNS
{
    public class GConnection
    {
        public Guid Id { get; set; }
        public string ConnectionID { get; set; }
        public int Turn { get; set; } = 1;
        public ICollection<GameCharacter> Characters { get; set; }
        public MapDataObject Map { get; set; }
        public ICollection<Enemy> Enemies { get; set; }
        public Player Player { get; set; }
        public Guid PlayerId { get; set; }
        public Battle? Battle { get; set; }
    }
}
