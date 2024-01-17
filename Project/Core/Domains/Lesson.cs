namespace Project.Core.Domains
{
    public class Lesson
    {
        public byte Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Index { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
    }
}
