namespace Project.Core.Domains
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public byte Index { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
    }
}
