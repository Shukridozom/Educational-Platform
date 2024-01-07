using Project.Core.Domains;

namespace Project.Core.Dtos
{
    public class EnrollmentDto
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
    }
}
