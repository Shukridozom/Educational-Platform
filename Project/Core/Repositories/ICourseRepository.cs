using Project.Core.Domains;
using Project.Core.Dtos;

namespace Project.Core.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Course GetCourseWithAuthor(int courseId);
        Course GetCourseWithLessons(int courseId);
        IEnumerable<CourseForAuthorsDto> GetAuthorCoursesWithEnrollmentsCount(int userId);
        IEnumerable<CourseForAuthorsDto> GetAuthorCoursesWithEnrollmentsCount(int userId, int pageIndex, int pageLength);
    }
}
