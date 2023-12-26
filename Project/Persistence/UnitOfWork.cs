using Project.Core;
using Project.Core.Repositories;

namespace Project.Persistence
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext context;
        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }
        public ICourseRepository Course { get; }
        public ILectureRepository Lectures { get; }
        public IEnrollmentRepository Enrollments { get; }

        public UnitOfWork(AppDbContext context,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            ICourseRepository courseRepository,
            ILectureRepository lectureRepository,
            IEnrollmentRepository enrollmentRepository
            )
        {
            this.context = context;
            Users = userRepository;
            Roles = roleRepository;
            Course = courseRepository;
            Lectures = lectureRepository;
            Enrollments = enrollmentRepository;
        }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
