﻿using System.ComponentModel.DataAnnotations;

namespace InfiniteCreativity.DTO
{
    public record LoginPlayerDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
