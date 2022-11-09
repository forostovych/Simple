using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Pesrson.UserModel
{
    public class User : Person
    {

        public User(string name, string login)
        {
            PersonID = Guid.NewGuid();
            Name = name;
            Login = login;
        }

        public void SetAccountID(Guid guid) => AccountID = guid;

    }
}
