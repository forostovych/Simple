using Simple.CardTable.CardDeckModel;
using Simple.CardTable.CardTableModel;
using Simple.PersonModel.PersonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.CardTable
{
    public interface ICardTableService
    {
        CardPlayer CreateCardPlayer(string name, decimal amount, PersonRole role = PersonRole.Player);
        void DealCardsToPlayers(int numberOfCads);

        int CalculateCardsWeight(CardDeck cardDeck);
    }
}
