using Project.Core.Repositories;

namespace Project.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        ICourseRepository Course { get; }
        ILectureRepository Lectures { get; }
        IEnrollmentRepository Enrollments { get; }
        int Complete();
    }
}
