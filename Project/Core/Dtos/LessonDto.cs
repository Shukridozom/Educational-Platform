using Project.Core.Domains;
using System.ComponentModel.DataAnnotations;

namespace Project.Core.Dtos
{
    public class LessonDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }
        [Required]
        [MaxLength(4096)]
        public string Body { get; set; }
        public byte Index { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}
