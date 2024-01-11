namespace Project.Core.Domains
{
    public class Transfer
    {
        public int Id { get; set; }
        public TransferType Type { get; set; }
        public byte TypeId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }

    }
}
