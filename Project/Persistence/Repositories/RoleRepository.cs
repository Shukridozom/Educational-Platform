using Project.Core.Domains;
using Project.Core.Repositories;

namespace Project.Persistence.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context)
            :base(context)
        {

        }
    }
}
