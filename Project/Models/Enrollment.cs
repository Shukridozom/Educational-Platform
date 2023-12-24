namespace Project.Models
{
    public class Enrollment
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
    }
}
