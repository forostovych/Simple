using Simple.Bank.AccountModels;
using Simple.PersonModel.PersonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Repository
{
    public static class Data
    {
        public static List<IAccount> Accounts { get; set; } = new List<IAccount>();
        public static List<IPerson> Persons { get; set; } = new List<IPerson>();
    }
}
