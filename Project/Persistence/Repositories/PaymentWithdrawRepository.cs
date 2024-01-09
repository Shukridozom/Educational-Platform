using Microsoft.EntityFrameworkCore;
using Project.Core.Domains;
using Project.Core.Repositories;

namespace Project.Persistence.Repositories
{
    public class PaymentWithdrawRepository : Repository<PaymentWithdraw>, IPaymentWithdrawRepository
    {
        public PaymentWithdrawRepository(AppDbContext context)
            :base(context)
        {

        }

        public double GetSystemBalance()
        {
            var transfers = Context.PaymentWithdraw.Include(t => t.Type).ToList();
            double balance = 0;
            foreach (var transfer in transfers)
                if (transfer.Type.Name == PaymentWithdrawTypeNames.Payment)
                    balance += transfer.Amount;
                else
                    balance -= transfer.Amount;

            return balance;
        }

    }
}
