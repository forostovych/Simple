using Simple.CardTableModel.CardModel;
using Simple.GamingTable.CardTableModel;

namespace Simple.GamingTable.CardDeckModel
{

    public class CardDeckService : ICardDeckService
    {

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
        public bool BlackJackOverPointCheck(CardDeck playerCardDeck)
        {
            return CalculateCardsWeight(playerCardDeck) > 21 ? true : false;
        }
        public int CalculateCardsWeight(CardDeck cardDeck)
        {
            int cardsWeight = 0;
            foreach (var card in cardDeck.Cards)
            {
                cardsWeight += ConvertCardToCardWeight(card);
            }

            if (cardsWeight > 21)
            {
                cardsWeight = 0;
                foreach (var card in cardDeck.Cards)
                {
                    cardsWeight += ConvertCardToCardWeightOverkill(card);
                }
            }
            return cardsWeight;
        }
        public void DealCardToPlayer(CardPlayer cardPlayer, int cards)
        {
            ICardDeckService ICardDeck = new CardDeckService();                             //      Add Interface CardDeckService
            (CardTable.TableCardDeck, cardPlayer.CardDeck) = ICardDeck.MoveCards(CardTable.TableCardDeck, cardPlayer.CardDeck, cards);
        }
        public bool BlackJackCheck(CardPlayer player) => CalculateCardsWeight(player.CardDeck) == 21;
        private int ConvertCardToCardWeightOverkill(Card card)
        {
            int cardRankValue;

            switch (card.Rank)
            {
                case Ranks.A: cardRankValue = 1; break;
                case Ranks.K: cardRankValue = 10; break;
                case Ranks.Q: cardRankValue = 10; break;
                case Ranks.J: cardRankValue = 10; break;
                case Ranks.Ten: cardRankValue = 10; break;
                case Ranks.Nine: cardRankValue = 9; break;
                case Ranks.Eight: cardRankValue = 8; break;
                case Ranks.Seven: cardRankValue = 7; break;
                case Ranks.Six: cardRankValue = 6; break;
                case Ranks.Five: cardRankValue = 5; break;
                case Ranks.Four: cardRankValue = 4; break;
                case Ranks.Three: cardRankValue = 3; break;
                case Ranks.Two: cardRankValue = 2; break;
                default: cardRankValue = 0; break;
            }

            return cardRankValue;
        }
        private int ConvertCardToCardWeight(Card card)
        {
            int cardRankValue;

            switch (card.Rank)
            {
                case Ranks.A: cardRankValue = 11; break;
                case Ranks.K: cardRankValue = 10; break;
                case Ranks.Q: cardRankValue = 10; break;
                case Ranks.J: cardRankValue = 10; break;
                case Ranks.Ten: cardRankValue = 10; break;
                case Ranks.Nine: cardRankValue = 9; break;
                case Ranks.Eight: cardRankValue = 8; break;
                case Ranks.Seven: cardRankValue = 7; break;
                case Ranks.Six: cardRankValue = 6; break;
                case Ranks.Five: cardRankValue = 5; break;
                case Ranks.Four: cardRankValue = 4; break;
                case Ranks.Three: cardRankValue = 3; break;
                case Ranks.Two: cardRankValue = 2; break;
                default: cardRankValue = 0; break;
            }

            return cardRankValue;
        }
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
