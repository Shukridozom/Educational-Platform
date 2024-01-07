using Microsoft.EntityFrameworkCore;
using Project.Core.Domains;
using Project.Core.Repositories;

namespace Project.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context)
            :base(context)
        {

        }

        public User GetStudentWithEnrollments(int userId)
        {
            return Context.Users.Include(u => u.Enrollments).SingleOrDefault(u => u.Id == userId);
        }

        public User GetUserWithRole(string username)
        {
            return Context.Users.Include(u => u.Role).SingleOrDefault(
                            u => u.Username.ToLower() == username.ToLower());
        }
    }
}
