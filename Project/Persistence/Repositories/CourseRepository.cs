using Project.Core.Domains;
using Project.Core.Repositories;

namespace Project.Persistence.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context)
            :base(context)
        {

        }
    }
}
