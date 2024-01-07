using Project.Core.Domains;

namespace Project.Core.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Course GetCourseWithAuthor(int courseId);
    }
}
