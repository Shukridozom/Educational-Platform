using Microsoft.EntityFrameworkCore;
using Project.Core;
using Project.Core.Repositories;
using Project.Persistence.Repositories;

namespace Project.Persistence
{
    public static class RepositoryPatternExtensions
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services, 
            IConfiguration config)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<ILectureRepository, LectureRepository>();
            services.AddTransient<IEnrollmentRepository, EnrollmentRepository>();
            services.AddTransient<ISystemVariablesRepository, SystemVariablesRepository>();
            services.AddTransient<ITransferRepository, TransferRepository>();
            services.AddTransient<ITransferTypeRepository, TransferTypeRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<AppDbContext>(opt => opt
                .UseMySQL(config.GetConnectionString("MySQL_Connection")));

            return services;
        
        }
    }
}
