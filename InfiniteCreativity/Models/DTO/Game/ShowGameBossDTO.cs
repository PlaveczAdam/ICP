using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniteCreativity.Models.DTO.Game
{
    public class ShowGameBossDTO:ShowGameEnemyDTO
    {
        public string Name { get; set; }
        public override double MaxHealth { get; set; }
    }
}
