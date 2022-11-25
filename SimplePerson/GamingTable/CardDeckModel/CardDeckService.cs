﻿using Simple.CardTableModel.CardModel;

namespace Simple.GamingTable.CardDeckModel
{
    public class CardDeckService : ICardDeckService
    {
        private CardDeck CreateCartDeck(int countDeck)
        {
            CardDeck NewcardDeck = new CardDeck();
            NewcardDeck.Cards = new Queue<Card>();

            for (int i = 0; i < countDeck; i++)
            {
                for (int s = 0; s < Enum.GetValues(typeof(Suits)).Length; s++)
                {
                    for (int r = 0; r < Enum.GetValues(typeof(Ranks)).Length; r++)
                    {
                        NewcardDeck.Cards.Enqueue(new Card((Ranks)r, (Suits)s));
                    }
                }
                //NewcardDeck.Cards.Enqueue(new Card(Ranks.Joker, Suits.Joker));
                //NewcardDeck.Cards.Enqueue(new Card(Ranks.Joker, Suits.Joker));
            }
            NewcardDeck.Id = Guid.NewGuid();

            return ShaffleCardDeck(NewcardDeck.Cards);
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

        private CardDeck ShaffleCardDeck(Queue<Card> cards)
        {
            CardDeck cardDeck = new CardDeck()
            {
                Cards = new Queue<Card>()
            };

            var shuffledCards = cards.OrderBy(_ => new Random().Next()).ToList();
            for (int i = 0; i < shuffledCards.Count - 1; i++)
                cardDeck.Cards.Enqueue(shuffledCards[i]);

            return cardDeck;
        }
    }
}