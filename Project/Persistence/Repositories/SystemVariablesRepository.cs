using Microsoft.EntityFrameworkCore;
using Project.Core.Domains;
using Project.Core.Repositories;

namespace Project.Persistence.Repositories
{
    public class SystemVariablesRepository : Repository<SystemVariables>, ISystemVariablesRepository
    {
        public SystemVariablesRepository(AppDbContext context)
            :base(context)
        {

        }

        public double GetProfitPercentageValue()
        {
            return Convert.ToDouble(Context.SystemVariables
                .SingleOrDefault(sv => sv.Name == SystemVariablesName.ProfitPercentage)?.Value);
        }
    }
}
