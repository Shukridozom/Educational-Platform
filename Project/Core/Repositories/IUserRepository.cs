using Project.Core.Domains;

namespace Project.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserWithRole(string username);
        User GetStudentWithEnrollments(int userId);
    }
}
