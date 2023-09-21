namespace InfiniteCreativity.DTO.Game
{
    public class ShowGameBossDTO : ShowGameEnemyDTO
    {
        public string Name { get; set; }
        public override double MaxHealth { get; set; }
    }
}
