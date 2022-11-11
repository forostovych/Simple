using Simple.CardTable.CardDeckModel;
using Simple.CardTable.CardTableModel;
using Simple.PersonModel.PersonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Testing_Console_UI
{
    public interface IConsole_UI
    {
        void ShowCardPlayerInfo(CardPlayer cardPlayer);
        void ShowCardDeck(CardDeck deck);
        public void ShowPlayerCardDeck(CardDeck deck, Person person);
        void ShowTransactionReport(string transactionStatus);
    }
}
