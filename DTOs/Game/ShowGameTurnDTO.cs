namespace InfiniteCreativity.DTO.Game
{
    public class ShowGameTurnDTO
    {
        public int Turn { get; set; }
        public Guid NextInTurnCharacterId { get; set; }
        public Guid? CutTreeHexTileId { get; set; }
    }
}
