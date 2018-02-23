using System.ComponentModel.DataAnnotations;

namespace CatServerSecondTime.Data
{
    public class Cat
    {
        private const int StringMaxLength = 50;
        public int Id { get; set; }

        [Required]
        [MaxLength(StringMaxLength)]
        public string Name { get; set; }

        [Range(0,30)]
        public int Age { get; set; }

        [Required]
        [MaxLength(StringMaxLength)]
        public string Bread { get; set; }

        [Required]
        [MaxLength(3000)]
        public string ImageUrl { get; set; }
    }
}
