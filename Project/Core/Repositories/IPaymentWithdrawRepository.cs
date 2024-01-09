using Project.Core.Domains;

namespace Project.Core.Repositories
{
    public interface IPaymentWithdrawRepository : IRepository<PaymentWithdraw>
    {
        double GetSystemBalance();
    }
}
