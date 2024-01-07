namespace Project.Core.Domains
{
    public class User
    {
        public User()
        {
            Courses = new List<Course>();
            Enrollments = new List<Enrollment>();
            PaymentWithdraws = new List<PaymentWithdraw>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public double Balance { get; set; }
        public IList<Course> Courses { get; set; }
        public IList<Enrollment> Enrollments { get; set; }
        public IList<PaymentWithdraw> PaymentWithdraws { get; set; }
    }
}
