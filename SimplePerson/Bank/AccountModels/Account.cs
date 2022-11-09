using Simple.Bank.TransactionModels;

namespace Simple.Bank.AccountModels
{
    public class Account
    {
        public Guid AccountID { get; set; }
        public Guid PersonID { get; set; }
        public List<ITransaction>? Transactions { get; set; }
    }
}
