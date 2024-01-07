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
    }
}
