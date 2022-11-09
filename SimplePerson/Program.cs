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

            IPerson elena = coreService.AddPlayer("Elena", PersonRole.Player, 50000);
            IPerson kitty = coreService.AddPlayer("Kitty", PersonRole.PlayerPro, 50000);

            ShowAllPersonsReport(Data.Persons);
            Console.WriteLine(  );


            IBankService bankService = new BankService();
            bankService.SendMoney(elena, kitty, 15000);

            ShowAllPersonsReport(Data.Persons);



        }

        static void ShowAllPersonsReport(List<IPerson> peoples)
        {
            PersonService personService = new PersonService();
            foreach (IPerson person in peoples)
            {
                Console.WriteLine(personService.GetPersonReport(person));
            }
        }

    }
}