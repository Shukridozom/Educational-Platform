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
    }
}
