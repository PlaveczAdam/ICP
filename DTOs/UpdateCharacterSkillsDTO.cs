using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class UpdateCharacterSkillsDTO
    {
        [Required]
        [MaxLength(5)]
        [MinLength(5)]
        public List<Guid?> Skills { get; set; }
    }
}
