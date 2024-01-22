using Project.Core.Domains;

namespace Project.Core.Repositories
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Lesson GetLessonWithCourse(int courseId, byte lessonId);
        IEnumerable<Lesson> GetLessonsOrderedByIndex(int courseId);
        IEnumerable<Lesson> GetLessonsOrderedByIndex(int courseId, int pageIndex, int pageLength);
    }
}
