using Simple.CardTable.CardDeckModel;
using Simple.CardTableModel.CardModel;

namespace Simple.CardTable.CardModel
{
    public class CardDeckService : ICardDeckService
    {
        private CardDeck CreateCartDeck(int countDeck)
        {
            CardDeck NewcardDeck = new CardDeck();
            NewcardDeck.Cards = new Queue<Card>();

            for (int i = 0; i < countDeck; i++)
            {
                for (int s = 0; s < Enum.GetValues(typeof(Suits)).Length - 1; s++)
                {
                    for (int r = 0; r < Enum.GetValues(typeof(Ranks)).Length - 1; r++)
                    {
                        NewcardDeck.Cards.Enqueue(new Card((Ranks)r, (Suits)s));
                    }
                }
                NewcardDeck.Cards.Enqueue(new Card(Ranks.Joker, Suits.Joker));
                NewcardDeck.Cards.Enqueue(new Card(Ranks.Joker, Suits.Joker));
            }
            NewcardDeck.Id = Guid.NewGuid();

            return NewcardDeck;
        }

        public CardDeck GetCardDeck(int count)
        {
            if (count == 0)
            {
                CardDeck cardDeck = new CardDeck();
                cardDeck.Cards = new Queue<Card>();
                return cardDeck;
            }

            return CreateCartDeck(count);
        }

        public (CardDeck, CardDeck) MoveCards(CardDeck cardDeckFrom, CardDeck cardDeckTo, int countOfCards)
        {
            for (int i = 0; i < countOfCards; i++)
            {
                cardDeckTo.Cards.Enqueue(cardDeckFrom.Cards.Dequeue());
            }
            return (cardDeckFrom, cardDeckTo);
        }
    }
}
