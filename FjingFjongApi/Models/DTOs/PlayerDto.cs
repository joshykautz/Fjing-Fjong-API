
using System;

namespace FjingFjongApi.Models
{
    public class PlayerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public string Image { get; set; }
    }
}
