using Simple.CardTable.CardDeckModel;

namespace Simple.CardTable
{
    public interface ICardDeckService
    {
        CardDeck GetCardDeck(int id);
        (CardDeck, CardDeck) MoveCards(CardDeck cardDeckFrom, CardDeck cardDeckTo, int countOfCards);

    }
}
