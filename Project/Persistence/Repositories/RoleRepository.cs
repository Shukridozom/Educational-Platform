using Microsoft.Extensions.Logging.Abstractions;
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

        public int GetAdminRoleId()
        {
            return Context.Roles.SingleOrDefault(r => r.Name == RoleName.Admin).Id;
        }

        public int GetAuthorRoleId()
        {
            return Context.Roles.SingleOrDefault(r => r.Name == RoleName.Author).Id;
        }

        public IEnumerable<Role> GetRolesExceptAdmin()
        {
            return Context.Roles.Where(r => r.Name != RoleName.Admin).ToList();
        }

        public int GetStudentRoleId()
        {
            return Context.Roles.SingleOrDefault(r => r.Name == RoleName.Student).Id;
        }
    }
}
