using Simple.Bank.AccountModels;
using Simple.CardTable.CardDeckModel;
using Simple.PersonModel.PersonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.CardTable.CardTableModel
{
    public abstract class ACardTableModel
    {
        public Person Person { get; set; }
        public Account Account { get; set; }
        public CardDeck CardDeck { get; set; }
    }
}
