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

        public Lesson GetLessonWithCourse(int courseId, byte lessonId)
        {
            return Context.Lessons.Include(l => l.Course).SingleOrDefault(l => l.Id == lessonId && l.CourseId == courseId);
        }
    }
}
