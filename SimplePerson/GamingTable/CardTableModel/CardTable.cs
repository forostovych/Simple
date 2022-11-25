using Simple.GamingTable.CardDeckModel;

namespace Simple.GamingTable.CardTableModel
{


    public class CardTable
    {

        public static List<CardPlayer> CardPlayers { get; set; } = new List<CardPlayer>();
        public static CardDeck TableCardDeck { get; set; } = new CardDeck();
        public static int DeskBet { get; set; }
        public static CardPlayer Dealer { get; set; }

    }


}
