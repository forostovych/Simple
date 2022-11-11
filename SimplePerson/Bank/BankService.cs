using Simple.Bank.AccountModels;
using Simple.Bank.TransactionModels;
using Simple.PersonModel.PersonModels;
using Simple;
using Simple.Bank;

namespace Simple.Bank
{
    public class BankService : IBankService
    {
        public void AddMoney(Person person, decimal amount)
        {
            Account account = GetAccountByPerson(person);

            Transaction newtransaction = new Transaction()
            {
                Data = DateTime.Now,
                FromID = Guid.NewGuid(),
                ToID = Guid.NewGuid(),
                TransactionAmonut = amount,
                Description = "Additing money"
            };

            account.Transactions.Add(newtransaction);
        }

        public Account CreateAccount(Person person)
        {
            Account account = new Account()
            {
                Id = Guid.NewGuid(),
                PersonID = person.Id,
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Data = DateTime.Now,
                        FromID = person.Id,
                        ToID = person.Id,
                        TransactionAmonut = 0,
                        Description = string.Empty
                    }
                }
            };
            person.AccountID = account.Id;

            return account;
        }
        public string CreateTransaction(Person personFrom, Person personTo, decimal amount)
        {
            Account accountFrom = GetAccountByPerson(personFrom);
            if (!IsEnougfMoney(personFrom, amount))
                return TransactionStatus.Reject.ToString();

            Account accountTo = GetAccountByPerson(personTo);
            SendMoney(personFrom, personTo, amount);

            return TransactionStatus.Success.ToString();
        }
        public void SendMoney(Person From, Person TO, decimal amount)
        {
            Account accountFrom = GetAccountByPerson(From);
            Account accountTo = GetAccountByPerson(TO);

            accountFrom.Transactions.Add (SendMoneyFrom(accountFrom, accountTo, amount));
            accountTo.Transactions.Add( SendMoneyTo(accountFrom, accountTo, amount));
        }
        private Transaction SendMoneyFrom(Account accountFrom, Account accountTO, decimal amount)
        {
            Transaction transaction =  new Transaction()
            {
                Data = DateTime.Now,
                FromID = accountFrom.Id,
                ToID = accountTO.Id,
                TransactionAmonut = amount * -1,
                Description = string.Empty
            };

            return transaction;
        }
        private Transaction SendMoneyTo(Account accountFrom, Account accountTO, decimal amount)
        {
            Transaction transaction = new Transaction()
            {
                Data = DateTime.Now,
                FromID = accountTO.Id,
                ToID = accountFrom.Id,
                TransactionAmonut = amount,
                Description = string.Empty
            };

            return transaction;
        }
        public Account GetAccountByPerson(Person person)
        {
            return Data.AccountRepository.entities.Where(x => x.Id.Equals(person.AccountID)).FirstOrDefault();
        }
        public decimal GetMoneyAmount(Account account)
        {
            return account.Transactions.Select(x => x.TransactionAmonut).Sum();
        }
        public bool IsEnougfMoney(Person account, decimal amount)
        {
            return GetMoneyAmount(GetAccountByPerson(account)) >= amount;
        }
        public decimal GetMoneyAmountByPerson(Person person)
        {
            return GetMoneyAmount(GetAccountByPerson(person));
        }

    }
}
