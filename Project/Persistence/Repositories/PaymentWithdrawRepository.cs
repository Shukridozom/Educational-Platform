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
    }
}
