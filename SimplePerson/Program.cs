using Simple.Bank;
using Simple.Core;
using Simple.PersonModel.PersonModels;
using Simple.PersonModel.PersonServices;
using Simple.Repository;

namespace SimplePerson
{
    public class Program
    {
        static void Main()
        {
            ICoreService coreService = new CoreService();

            Person elena = coreService.AddPlayer("Elena", PersonRole.Player, 50000);
            Person kitty = coreService.AddPlayer("Kitty", PersonRole.PlayerPro, 50000);

            ShowAllPersonsReport(Data.PersonRepository.entities);
            Console.WriteLine(  );

            IBankService bankService = new BankService();
            bankService.SendMoney(elena, kitty, 15000);

            ShowAllPersonsReport(Data.PersonRepository.entities);
        }

        static void ShowAllPersonsReport(List<Person> peoples)
        {
            PersonService personService = new PersonService();
            foreach (Person person in peoples)
            {
                Console.WriteLine(personService.GetPersonReport(person));
            }
        }

    }
}