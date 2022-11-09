using ConsoleApp_Pesrson;
using ConsoleApp_Pesrson.UserModel;
using System;

namespace ConsoleApp_Person
{

    public class Program
    {
        static void Main()
        {

            IPerson violeta = new User("Violeta", "Viola");
            List<IPerson> peoples = new List<IPerson>();
            peoples.Add( violeta );

            peoples[0].AccountID = Guid.NewGuid();

            IPersonService perService = new PersonService();

            Console.WriteLine(perService.GetPersonReport(peoples[0]));


        }
    }
}