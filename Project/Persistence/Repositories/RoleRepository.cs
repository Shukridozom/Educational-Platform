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

        public IEnumerable<Role> GetRolesExceptAdmin()
        {
            return Context.Roles.Where(r => r.Name != RoleName.Admin).ToList();
        }
    }
}
