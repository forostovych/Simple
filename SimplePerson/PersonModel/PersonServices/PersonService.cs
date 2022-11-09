using Simple.Bank;
using Simple.PersonModel.PersonModels;

namespace Simple.PersonModel.PersonServices
{
    public class PersonService : IPersonService
    {
        public Person CreatePerson(string name, PersonRole role)
        {
            Person person = new Person
            {
                Name = name,
                Role = role,
                Id = Guid.NewGuid(),
                GamesCount = 0,
                WinGamesCount = 0
            };

            return person;
        }

        public string GetPersonReport(Person person)
        {
            IBankService bankService = new BankService();
            string report = string.Empty;
            {
                report = $"Name: {person.Name}\n";
                report += $"ID: {person.Id}\n";
                report += $"Account ID:  {person.AccountID}\n";
                report += $"Total Games: {person.GamesCount}\n";
                report += $"Wins: {person.WinGamesCount}\n";
                report += $"Role: {person.Role}\n";
                report += $"Money Amount {bankService.GetMoneyAmountByPerson(person)}\n";
            }

            return report;
        }
    }
}
