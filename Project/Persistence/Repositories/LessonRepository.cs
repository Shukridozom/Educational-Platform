using Microsoft.EntityFrameworkCore;
using Project.Core.Domains;
using Project.Core.Repositories;

namespace Project.Persistence.Repositories
{
    public class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        public LessonRepository(AppDbContext context)
            :base(context)
        {

        }

        public IEnumerable<Lesson> GetLessonsOrderedByIndex(int courseId)
        {

            return Context.Lessons
                .Where(l => l.CourseId == courseId)
                .OrderBy(l => l.Index)
                .ToList();
        }

        public IEnumerable<Lesson> GetLessonsOrderedByIndex(int courseId, int pageIndex, int pageLength)
        {
            if (pageIndex <= 0 || pageLength <= 0)
                return GetLessonsOrderedByIndex(courseId);

            return Context.Lessons
                .Where(l => l.CourseId == courseId)
                .OrderBy(l => l.Index)
                .Skip((pageIndex - 1) * pageLength)
                .Take(pageLength)
                .ToList();
        }

        public Lesson GetLessonWithCourse(int courseId, byte lessonId)
        {
            return Context.Lessons.Include(l => l.Course).SingleOrDefault(l => l.Id == lessonId && l.CourseId == courseId);
        }
    }
}
