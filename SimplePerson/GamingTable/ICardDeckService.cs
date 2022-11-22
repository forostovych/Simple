using Simple.GamingTable.CardDeckModel;

namespace Simple.GamingTable
{
    public interface ICardDeckService
    {
        CardDeck GetCardDeck(int id);
        (CardDeck, CardDeck) MoveCards(CardDeck cardDeckFrom, CardDeck cardDeckTo, int countOfCards);

    }
}
