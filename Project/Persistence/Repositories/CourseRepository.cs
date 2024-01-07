using Microsoft.EntityFrameworkCore;
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

        public Course GetCourseWithAuthor(int courseId)
        {
            return Context.Courses.Include(c => c.User).SingleOrDefault(c => c.Id == courseId);
        }
    }
}
