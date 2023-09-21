namespace InfiniteCreativity.Models.CoreNS
{
    public class CharacterSkillSlot
    {
        public Guid Id { get; set; }
        public Character Character { get; set; }
        public SkillHolder? SkillHolder { get; set; }
        public int SlotNumber { get; set; }
    }
}
