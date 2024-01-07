using Project.Core.Domains;
using Project.Core.Repositories;

namespace Project.Persistence.Repositories
{
    public class PaymentWithdrawTypeRepository : Repository<PaymentWithdrawType>, IPaymentWithdrawTypeRepository
    {
        public PaymentWithdrawTypeRepository(AppDbContext context)
            :base(context)
        {

        }
    }
}
