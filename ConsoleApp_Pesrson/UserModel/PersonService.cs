using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Pesrson.UserModel
{
    public class PersonService : IPersonService
    {
        public string GetPersonReport(IPerson person)
        {
            string result = string.Empty;
            {
                result += $"ID: {person.PersonID}\n";
                result += $"AccountID: {person.AccountID}\n";
                result += $"Name: {person.Name}\n";
                result += $"Login: {person.Login} \n";
                result += $"Win: {person.WinCount} \n";
                result += $"Louse: {person.LouseCount}\n";
            }

            return result;
        }
    }
}
