namespace Project.Core.Domains
{
    public class Enrollment
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public double AdminPortionOfPrice { get; set; }
    }
}
