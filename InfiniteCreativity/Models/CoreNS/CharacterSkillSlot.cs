namespace InfiniteCreativity.Models.CoreNS
{
    public class CharacterSkillSlot
    {
        private int _currentCooldown;

        public Guid Id { get; set; }
        public Character Character { get; set; }
        public SkillHolder? SkillHolder { get; set; }
        public int SlotNumber { get; set; }
        public int CurrentCooldown { get => _currentCooldown; set {
                if (value < 0)
                {
                    _currentCooldown = 0;
                } else
                {
                    _currentCooldown = value;
                }
            }
        }
    }
}
