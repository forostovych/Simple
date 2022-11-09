using Simple.PersonModel.PersonModels;
using Simple.PersonModel.PersonServices;

namespace SimplePerson
{
    public class Program
    {
        static void Main()
        {

            List<IPerson> peoples = new List<IPerson>();
            PersonService personService = new PersonService();

            peoples.Add(personService.CreatePerson("Bonya", PersonRole.PlayerPro));
            peoples.Add(personService.CreatePerson("Tomas", PersonRole.Player));
            peoples.Add(personService.CreatePerson("Simba", PersonRole.Administrator));
            peoples.Add(personService.CreatePerson("Slava", PersonRole.Administrator));


            ShowAllPersons(peoples);


        }

        static void ShowAllPersons(List<IPerson> peoples)
        {
            PersonService personService = new PersonService();
            foreach (IPerson person in peoples)
            {
                Console.WriteLine(personService.GetPersonReport(person));
            }
        }

    }
}