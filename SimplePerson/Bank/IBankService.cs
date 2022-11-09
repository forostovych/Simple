using Simple.Bank.AccountModels;
using Simple.Bank.TransactionModels;
using Simple.PersonModel.PersonModels;

namespace Simple.Bank
{
    public interface IBankService
    {
        IAccount CreateAccount(IPerson person);
        string CreateTransaction(IPerson personFrom, IPerson personTo, decimal amount);
        bool IsEnougfMoney(IAccount account, decimal amount);
        IAccount GetAccountByPerson(IPerson person);
        decimal GetMoneyAmount(IAccount account);
    }
}
