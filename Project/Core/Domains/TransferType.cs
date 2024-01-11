namespace Project.Core.Domains
{
    public class TransferType
    {
        public TransferType()
        {
            PaymentWithdraws = new List<Transfer>();
        }
        public byte Id { get; set; }
        public string Name { get; set; }
        public IList<Transfer> PaymentWithdraws { get; set; }
    }
}
