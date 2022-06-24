using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Dog
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [StringLength(10)]
        public string Breed { get; set; }
        [MaxLength(100)]
        public string Notes { get; set; }
        public string ImageUrl { get; set; }

        public Owner Owner { get; set; }

    }
}
