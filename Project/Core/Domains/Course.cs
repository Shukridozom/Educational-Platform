namespace Project.Core.Domains
{
    public class Course
    {
        public Course()
        {
            Lectures = new List<Lecture>();
            Enrollments = new List<Enrollment>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public IList<Lecture> Lectures { get; set; }
        public IList<Enrollment> Enrollments { get; set; }
    }
}
