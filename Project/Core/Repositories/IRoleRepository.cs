using Project.Core.Domains;

namespace Project.Core.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        IEnumerable<Role> GetRolesExceptAdmin();
        int GetAdminRoleId();
        int GetAuthorRoleId();
        int GetStudentRoleId();
    }
}
