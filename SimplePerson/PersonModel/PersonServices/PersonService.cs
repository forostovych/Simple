using Simple.PersonModel.PersonModels;

namespace Simple.PersonModel.PersonServices
{
    public class PersonService : IPersonService
    {
        public IPerson CreatePerson(string name, PersonRole role)
        {
            IPerson person = new Person
            {
                Name = name,
                Role = role,
                ID = Guid.NewGuid(),
                GamesCount = 0,
                WinGamesCount = 0
            };

            return person;
        }

        public string GetPersonReport(IPerson person)
        {
            string report = string.Empty;
            {
                report = $"Name: {person.Name}\n";
                report += $"ID: {person.ID}\n";
                report += $"Account ID:  {person.AccountID}\n";
                report += $"Total Games: {person.GamesCount}\n";
                report += $"Wins: {person.WinGamesCount}\n";
                report += $"Role: {person.Role}\n";
            }

            return report;
        }
    }
}
