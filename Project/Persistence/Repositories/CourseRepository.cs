using Microsoft.EntityFrameworkCore;
using Project.Core.Domains;
using Project.Core.Dtos;
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


        public Course GetCourseWithLessons(int courseId)
        {
            var course = Context.Courses.Include(c => c.Lessons).SingleOrDefault(c => c.Id == courseId);
            course.Lessons = course.Lessons.OrderBy(l => l.Index).ToList();
            
            return course;
        }

        public IEnumerable<CourseWithEnrollmentsCountDto> GetAuthorCoursesWithEnrollmentsCount(int userId)
        {
            return Context.Courses
                .Include(c => c.Enrollments)
                .Where(c => c.UserId == userId)
                .Select(c => new CourseWithEnrollmentsCountDto()
                {
                    NummberOfEnrollments = c.Enrollments.Count(),
                    CourseDto = new CourseDto()
                    {
                        Id = c.Id,
                        AuthorId = c.UserId,
                        Title = c.Title,
                        Description = c.Description,
                        Price = c.Price,
                        Date = c.Date
                    }
                })
                .ToList();
        }
    }
}
