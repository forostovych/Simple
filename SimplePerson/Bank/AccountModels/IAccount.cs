using Simple.Bank.TransactionModels;

namespace Simple.Bank.AccountModels
{
    public interface IAccount
    {
        Guid AccountID { get; }
        Guid PersonID { get; }
        List<ITransaction> Transactions { get; set; }
    }
}

