using System;
using System.ComponentModel.DataAnnotations;

namespace FjingFjongApi.Models
{
    public class Match
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int TeamOneScore { get; set; }
        [Required]
        public int TeamTwoScore { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Guid PlayerOneId { get; set; }
        public Guid PlayerTwoId { get; set; }
        public Guid PlayerThreeId { get; set; }
        public Guid PlayerFourId { get; set; }

        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public Player PlayerThree { get; set; }
        public Player PlayerFour { get; set; }
    }
}
