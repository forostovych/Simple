using Simple.CardTable.CardDeckModel;
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

        void ShowCardDeck(CardDeck deck);
        public void ShowPlayerCardDeck(CardDeck deck, Person person);

    }
}
