using Simple.CardTable.CardDeckModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.CardTable.CardTableModel
{
    public class CardDesk
    {
        public static List<CardPlayer> CardPlayers { get; set; } = new List<CardPlayer>();

        public static CardDeck TableCardDeck { get; set; } = new CardDeck();
    }
}
