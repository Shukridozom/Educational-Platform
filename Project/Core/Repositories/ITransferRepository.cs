using Project.Core.Domains;

namespace Project.Core.Repositories
{
    public interface ITransferRepository : IRepository<Transfer>
    {
        double GetSystemBalance();
    }
}
