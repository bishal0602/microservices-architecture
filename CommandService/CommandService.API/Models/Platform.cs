﻿using System.ComponentModel.DataAnnotations;

namespace CommandService.API.Models
{
    public class Platform
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid ExternalId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Command> Commands { get; set; } = new List<Command>();
    }
}
