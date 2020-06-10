using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FjingFjongApi.Models
{
    public class Player
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public double Rating { get; set; }
        public string Image { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public ICollection<Match> MatchesOne { get; set; }
        public ICollection<Match> MatchesTwo { get; set; }
        public ICollection<Match> MatchesThree { get; set; }
        public ICollection<Match> MatchesFour { get; set; }
    }
}
