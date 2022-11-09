using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Pesrson.UserModel
{
    public interface IPersonService
    {
        public string GetPersonReport(IPerson person);

    }
}
