using Simple.Bank.AccountModels;
using Simple.Bank.TransactionModels;
using Simple.PersonModel.PersonModels;

namespace Simple.Bank
{
    public interface IBankService
    {
        Account CreateAccount(Person person);
        string CreateTransaction(Person personFrom, Person personTo, decimal amount);
        bool IsEnoughMoney(Person account, decimal amount);
        Account GetAccountByPerson(Person person);
        decimal GetMoneyAmount(Account account);
        string SendMoney(Person From, Person TO, decimal amount);
        decimal GetMoneyAmountByPerson(Person person);
        void AddMoney(Person person, decimal amount);
    }
}
