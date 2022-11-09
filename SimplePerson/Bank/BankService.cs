using Simple.Bank.AccountModels;
using Simple.Bank.TransactionModels;
using Simple.PersonModel.PersonModels;
using Simple.Repository;
using Simple.Bank;

namespace Simple.Bank
{
    public class BankService : IBankService
    {
        public void AddMoney(IPerson person, decimal amount)
        {
            IAccount account = GetAccountByPerson(person);

            ITransaction newtransaction = new Transaction()
            {
                Data = DateTime.Now,
                FromID = Guid.NewGuid(),
                ToID = Guid.NewGuid(),
                TransactionAmonut = amount,
                Description = "Additing money"
            };

            account.Transactions.Add(newtransaction);
        }

        public IAccount CreateAccount(IPerson person)
        {
            IAccount account = new Account()
            {
                AccountID = Guid.NewGuid(),
                PersonID = person.ID,
                Transactions = new List<ITransaction>()
                {
                    new Transaction()
                    {
                        Data = DateTime.Now,
                        FromID = person.ID,
                        ToID = person.ID,
                        TransactionAmonut = 0,
                        Description = string.Empty
                    }
                }
            };
            person.AccountID = account.AccountID;

            return account;
        }
        public string CreateTransaction(IPerson personFrom, IPerson personTo, decimal amount)
        {
            IAccount accountFrom = GetAccountByPerson(personFrom);
            if (!IsEnougfMoney(personFrom, amount))
                return TransactionStatus.Reject.ToString();


            IAccount accountTo = GetAccountByPerson(personTo);

            SendMoney(personFrom, personTo, amount);

            return TransactionStatus.Success.ToString();
        }
        public void SendMoney(IPerson From, IPerson TO, decimal amount)
        {
            IAccount accountFrom = GetAccountByPerson(From);
            IAccount accountTo = GetAccountByPerson(TO);

            accountFrom.Transactions.Add (SendMoneyFrom(accountFrom, accountTo, amount));
            accountTo.Transactions.Add( SendMoneyTo(accountFrom, accountTo, amount));
        }
        private ITransaction SendMoneyFrom(IAccount accountFrom, IAccount accountTO, decimal amount)
        {
            ITransaction transaction =  new Transaction()
            {
                Data = DateTime.Now,
                FromID = accountFrom.AccountID,
                ToID = accountTO.AccountID,
                TransactionAmonut = amount * -1,
                Description = string.Empty
            };

            return transaction;
        }
        private ITransaction SendMoneyTo(IAccount accountFrom, IAccount accountTO, decimal amount)
        {
            ITransaction transaction = new Transaction()
            {
                Data = DateTime.Now,
                FromID = accountTO.AccountID,
                ToID = accountFrom.AccountID,
                TransactionAmonut = amount,
                Description = string.Empty
            };

            return transaction;
        }
        public IAccount GetAccountByPerson(IPerson person)
        {
            return Data.Accounts.Where(x => x.AccountID.Equals(person.AccountID)).FirstOrDefault();
        }
        public decimal GetMoneyAmount(IAccount account)
        {
            return account.Transactions.Select(x => x.TransactionAmonut).Sum();
        }
        public bool IsEnougfMoney(IPerson account, decimal amount)
        {
            return GetMoneyAmount(GetAccountByPerson(account)) >= amount;
        }
        public decimal GetMoneyAmountByPerson(IPerson person)
        {
            return GetMoneyAmount(GetAccountByPerson(person));
        }

    }
}
