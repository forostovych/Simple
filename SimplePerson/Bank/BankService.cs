using Simple.Bank.AccountModels;
using Simple.Bank.TransactionModels;
using Simple.PersonModel.PersonModels;
using Simple;
using Simple.Bank;
using Simple.Testing_Console_UI;

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
                ToID = account.Id,
                TransactionAmount = amount,
                Description = "Additing money"
            };

            account.Transactions.Add(newtransaction);
        }

        public void RemoveMoney(Person person, decimal amount)
        {
            Account account = GetAccountByPerson(person);

            Transaction newtransaction = new Transaction()
            {
                Data = DateTime.Now,
                FromID = account.Id,
                ToID = Guid.NewGuid(),
                TransactionAmount = amount * -1,
                Description = "Removing money"
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
                        TransactionAmount = 0,
                        Description = string.Empty
                    }
                }
            };
            person.AccountID = account.Id;

            return account;
        }
        public string CreateTransaction(Person personFrom, Person personTo, decimal amount)
        {
            if (!IsEnoughMoney(personFrom, amount))
            {
                return TransactionStatus.Reject.ToString();
            }
            Account accountFrom = GetAccountByPerson(personFrom);
            Account accountTo = GetAccountByPerson(personTo);
            accountFrom.Transactions.Add(SendMoneyFrom(accountFrom, accountTo, amount));
            accountTo.Transactions.Add(SendMoneyTo(accountFrom, accountTo, amount));

            return TransactionStatus.Success.ToString();
        }
        public string SendMoney(Person From, Person TO, decimal amount)
        {
            string transactionReport = CreateTransaction(From, TO, amount);

            return $"" +
                $"|| Sender: [{From.Name}] ||" +
                $" Amount: [~ {amount}$ ~] ||" +
                $" Getter: [{TO.Name}] ||" +
                $" Status: ==[{transactionReport}]==";

        }
        private Transaction SendMoneyFrom(Account accountFrom, Account accountTO, decimal amount)
        {
            Transaction transaction =  new Transaction()
            {
                Data = DateTime.Now,
                FromID = accountFrom.Id,
                ToID = accountTO.Id,
                TransactionAmount = amount * -1,
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
                TransactionAmount = amount,
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
            return account.Transactions.Select(x => x.TransactionAmount).Sum();
        }
        public bool IsEnoughMoney(Person account, decimal amount)
        {
            return GetMoneyAmount(GetAccountByPerson(account)) >= amount;
        }
        public decimal GetMoneyAmountByPerson(Person person)
        {
            return GetMoneyAmount(GetAccountByPerson(person));
        }

    }
}
