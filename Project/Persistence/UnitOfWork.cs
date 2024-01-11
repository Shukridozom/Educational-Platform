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
        public ISystemVariablesRepository SystemVariables { get; set; }
        public ITransferRepository Transfers { get; }
        public ITransferTypeRepository TransferTypes { get; }

        public UnitOfWork(AppDbContext context,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            ICourseRepository courseRepository,
            ILectureRepository lectureRepository,
            IEnrollmentRepository enrollmentRepository,
            ISystemVariablesRepository systemVariablesRepository,
            ITransferRepository transferRepository,
            ITransferTypeRepository transferTypeRepository
            )
        {
            this.context = context;
            Users = userRepository;
            Roles = roleRepository;
            Course = courseRepository;
            Lectures = lectureRepository;
            Enrollments = enrollmentRepository;
            SystemVariables = systemVariablesRepository;
            Transfers = transferRepository;
            TransferTypes = transferTypeRepository;
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
