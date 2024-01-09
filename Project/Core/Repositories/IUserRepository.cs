using Project.Core.Domains;

namespace Project.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserWithRole(string username);
        User GetUserWithRole(int id);
        User GetStudentWithEnrollments(int userId);
    }
}
