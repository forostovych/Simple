using Simple.Bank;
using Simple.Bank.AccountModels;
using Simple.PersonModel.PersonModels;
using Simple.PersonModel.PersonServices;

namespace Simple.Core
{
    public class CoreService : ICoreService
    {
        public Person AddPlayer(string name, decimal amount, PersonRole role = PersonRole.Player)
        {
            IPersonService personService = new PersonService();
            IBankService bankService = new BankService();

            Person person = personService.CreatePerson(name, role);
            Account account = bankService.CreateAccount(person);
            Data.AccountRepository.Add(account);
            Data.PersonRepository.Add(person);
            bankService.AddMoney(person, amount);

            return person;
        }

        public string GetAccountReportByPerson(Person person)
        {
            IBankService bankService = new BankService();
            return $"Name: {person.Name}\n" +
                $"Role: {person.Role}\n" +
                $"Games Count: {person.GamesCount}\n" +
                $"Money Amount: {bankService.GetMoneyAmountByPerson(person)} $\n";
        }
    }
}
