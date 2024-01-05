using Project.Core.Domains;

namespace Project.Core.Repositories
{
    public interface ISystemVariablesRepository : IRepository<SystemVariables>
    {
        double GetProfitPercentageValue();
    }
}
