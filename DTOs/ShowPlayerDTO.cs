﻿namespace InfiniteCreativity.DTO
{
    public class ShowPlayerDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CharacterSlot { get; set; }
        public int QuestSlot { get; set; }
        public IEnumerable<ShowItemDTO>? Inventory { get; set; }
        public IEnumerable<ShowCharacterDTO> Characters { get; set; }
    }
}
