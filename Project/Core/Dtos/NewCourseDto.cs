using Project.Core.Domains;
using System.ComponentModel.DataAnnotations;

namespace Project.Core.Dtos
{
    public class NewCourseDto
    {
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }
        [Required]
        [MaxLength(512)]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
