namespace Simple.Bank.TransactionModels
{
    public class Transaction : ITransaction
    {
        public DateTime Deta { get; set; }
        public Guid FromID { get; set; }
        public Guid ToID { get; set; }
        public decimal TransactionAmonut { get; set; }
        public string Description { get; set; }

    }
}
