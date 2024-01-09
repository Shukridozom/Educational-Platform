using Project.Core.Domains;

namespace Project.Core.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public int AuthorId { get; set; }
    }
}
