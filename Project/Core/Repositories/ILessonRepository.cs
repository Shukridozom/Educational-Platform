using Project.Core.Domains;

namespace Project.Core.Repositories
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Lesson GetLessonWithCourse(int courseId, byte lessonId);
    }
}
