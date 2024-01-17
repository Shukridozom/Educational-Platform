using Project.Core.Domains;
using Project.Core.Repositories;

namespace Project.Persistence.Repositories
{
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(AppDbContext context)
            :base(context)
        {

        }

        public IList<int> GetStudentEnrollmentsKeyList(int userId)
        {
            return Context.Enrollments.Where(en => en.UserId == userId).Select(en => en.CourseId).ToList();
        }

        public double GetSystemProfit()
        {
            var enrollments = Context.Enrollments.ToList();
            double profit = 0;
            foreach (var enrollment in enrollments)
                profit += enrollment.AdminPortionOfPrice;

            return profit;
        }

        public bool IsEnrolled(int userId, int courseId)
        {
            return Context.Enrollments.SingleOrDefault(en => en.UserId == userId && en.CourseId == courseId) != null;
        }
    }
}
