﻿using Project.Core.Repositories;

namespace Project.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        ICourseRepository Course { get; }
        ILessonRepository Lessons { get; }
        IEnrollmentRepository Enrollments { get; }
        ISystemVariablesRepository SystemVariables { get; }
        ITransferRepository Transfers { get; }
        ITransferTypeRepository TransferTypes { get; }
        int Complete();
    }
}
