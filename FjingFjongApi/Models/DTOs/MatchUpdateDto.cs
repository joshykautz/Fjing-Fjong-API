using System;

namespace FjingFjongApi.Models
{
    public class MatchUpdateDto
    {
        public Guid Id { get; set; }
        public int? TeamOneScore { get; set; }
        public int? TeamTwoScore { get; set; }
        public Guid PlayerOneId { get; set; }
        public Guid PlayerTwoId { get; set; }
        public Guid PlayerThreeId { get; set; }
        public Guid PlayerFourId { get; set; }
    }
}
