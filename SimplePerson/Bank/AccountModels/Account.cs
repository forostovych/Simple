using Simple.Bank.TransactionModels;

namespace Simple.Bank.AccountModels
{
    public class Account : BaseModel
    {
        public Guid PersonID { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}