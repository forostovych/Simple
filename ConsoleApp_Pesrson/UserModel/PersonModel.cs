using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Pesrson.UserModel
{
    public abstract class Person : IPerson
    {

        public Guid PersonID { get; set; }
        public Guid AccountID { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public int WinCount { get; set; }
        public int LouseCount { get; set; }

    }
}
