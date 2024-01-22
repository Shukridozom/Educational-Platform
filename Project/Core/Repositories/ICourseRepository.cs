using Project.Core.Domains;
using Project.Core.Dtos;

namespace Project.Core.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Course GetCourseWithAuthor(int courseId);
        Course GetCourseWithLessons(int courseId);
        IEnumerable<CourseWithEnrollmentsCountDto> GetAuthorCoursesWithEnrollmentsCount(int userId);
    }
}
