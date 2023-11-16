using DTOs.Enums.GameNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Game
{
    public class ShowGameMinionDTO
    {
        public Guid Id { get; set; }
        public ShowBattleParticipantDTO Caster { get; set; }
        public int? CurrentDuration { get; set; }
        public Side Side { get; set; }
        public double CurrentHealth { get; set; }

        public string Name { get; set; }
        public double MaxHealth { get; set; }
        public MinionType Type { get; set; }
        public int? Duration { get; set; }
        public double Defense { get; set; }
        public double CriticalChance { get; set; }
        public double CriticalMultiplier { get; set; }
        public double Damage { get; set; }
        public double Speed { get; set; }
    }
}
