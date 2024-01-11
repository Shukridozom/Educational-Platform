using Project.Core.Domains;
using Project.Core.Repositories;

namespace Project.Persistence.Repositories
{
    public class TransferTypeRepository : Repository<TransferType>, ITransferTypeRepository
    {
        public TransferTypeRepository(AppDbContext context)
            :base(context)
        {

        }
    }
}
