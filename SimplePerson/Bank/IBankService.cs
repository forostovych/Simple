using Simple.Bank.AccountModels;
using Simple.Bank.TransactionModels;
using Simple.PersonModel.PersonModels;

namespace Simple.Bank
{
    public interface IBankService
    {

        IAccount CreateAccount(IPerson person);
        string CreateTransaction(IPerson personFrom, IPerson personTo, decimal amount);
        bool IsEnougfMoney(IPerson account, decimal amount);
        IAccount GetAccountByPerson(IPerson person);
        decimal GetMoneyAmount(IAccount account);
        void SendMoney(IPerson From, IPerson TO, decimal amount);
        decimal GetMoneyAmountByPerson(IPerson person);
        void AddMoney(IPerson person, decimal amount);

    }
}
