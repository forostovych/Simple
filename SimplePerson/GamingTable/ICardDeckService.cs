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
        void DealCardsToPlayers(int numberOfCads);
        void DealCardToPlayer(CardPlayer player);
        bool BlackJackCheck(CardPlayer player);
    }
}
