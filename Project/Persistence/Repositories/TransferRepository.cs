using Microsoft.EntityFrameworkCore;
using Project.Core.Domains;
using Project.Core.Repositories;

namespace Project.Persistence.Repositories
{
    public class TransferRepository : Repository<Transfer>, ITransferRepository
    {
        public TransferRepository(AppDbContext context)
            :base(context)
        {

        }

        public double GetSystemBalance()
        {
            var transfers = Context.Transfer.Include(t => t.Type).ToList();
            double balance = 0;
            foreach (var transfer in transfers)
                if (transfer.Type.Name == TransferTypeNames.Payment)
                    balance += transfer.Amount;
                else
                    balance -= transfer.Amount;

            return balance;
        }

    }
}
