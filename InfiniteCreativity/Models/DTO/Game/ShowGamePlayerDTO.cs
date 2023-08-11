﻿namespace InfiniteCreativity.Models.DTO.Game
{
    public class ShowGamePlayerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CharacterSlot { get; set; }
        public int QuestSlot { get; set; }
        public IEnumerable<ShowItemDTO>? Inventory { get; set; }
        public IEnumerable<ShowGameCharacterDTO> Characters { get; set; }
    }
}
