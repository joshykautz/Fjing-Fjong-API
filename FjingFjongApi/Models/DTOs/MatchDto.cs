using System;

namespace FjingFjongApi.Models
{
    public class MatchDto
    {
        public Guid Id { get; set; }
        public int TeamOneScore { get; set; }
        public int TeamTwoScore { get; set; }
        public PlayerDto PlayerOne { get; set; }
        public PlayerDto PlayerTwo { get; set; }
        public PlayerDto PlayerThree { get; set; }
        public PlayerDto PlayerFour { get; set; }
    }
}
