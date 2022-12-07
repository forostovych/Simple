using Simple.GamingTable.CardDeckModel;
using Simple.GamingTable.CardTableModel;

namespace Simple.GamingTable
{
    public interface ICardDeckService
    {
        CardDeck GetCardDeck(int id);
        (CardDeck, CardDeck) MoveCards(CardDeck cardDeckFrom, CardDeck cardDeckTo, int countOfCards);
        bool BlackJackOverPointCheck(CardDeck playerCardDeck);
        int CalculateCardsWeight(CardDeck cardDeck);
        void DealCardToPlayer(CardPlayer cardPlayer, int cards);
        bool BlackJackCheck(CardPlayer player);
    }
}
