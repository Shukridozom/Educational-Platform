namespace Project.Core.Domains
{
    public class PaymentWithdrawType
    {
        public PaymentWithdrawType()
        {
            PaymentWithdraws = new List<PaymentWithdraw>();
        }
        public byte Id { get; set; }
        public string Name { get; set; }
        public IList<PaymentWithdraw> PaymentWithdraws { get; set; }
    }
}
