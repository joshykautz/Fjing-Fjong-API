using System.ComponentModel.DataAnnotations;

namespace FjingFjongApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
