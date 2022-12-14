using System;
using Simple.CardTable.CardDeckModel;
using Simple.CardTableModel.CardModel;
using Simple.PersonModel.PersonModels;

namespace Simple.Testing_Console_UI
{
    public class Console_UI : IConsole_UI
    {
        public void ShowCardDeck(CardDeck deck)
        {
            foreach (var card in deck.Cards)
            {
                ShowCard(card);
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void ShowPlayerCardDeck(CardDeck deck, Person person)
        {
            foreach (var card in deck.Cards)
            {
                ShowCard(card);
                var memColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("|");
                Console.ForegroundColor= memColor;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        private void ShowCard(Card card)
        {
            Console.ForegroundColor = GetCardSuitColor( card );
            Console.Write($"{GetCardSuitChar(card)}");
            Console.Write($"{GetCardRankValue(card)}");
        }
        private ConsoleColor GetCardSuitColor(Card card)
        {
            switch (card.Suit)
            {
                case Suits.Hearts: return ConsoleColor.DarkRed; 
                case Suits.Diamonds: return ConsoleColor.Red; 
                case Suits.Spades: return ConsoleColor.DarkGray;
                case Suits.Clubs: return ConsoleColor.DarkCyan;
                case Suits.Joker: return ConsoleColor.Yellow;
                default: return ConsoleColor.White;
            }
        }
        private char GetCardSuitChar(Card card)
        {
            char cardSuitChar = (char)card.Suit;
            switch (card.Suit)
            {
                case Suits.Hearts: cardSuitChar = '♥'; break;
                case Suits.Diamonds: cardSuitChar = '♦'; break;
                case Suits.Clubs: cardSuitChar = '♣'; break;
                case Suits.Spades: cardSuitChar = '♠'; break;

                default: break;
            }

            return cardSuitChar;
        }
        private string GetCardRankValue(Card card)
        {
            string cardRankValue = string.Empty;
            switch (card.Rank)
            {
                case Ranks.Ten: cardRankValue = "10"; break;
                case Ranks.Nine: cardRankValue = "9"; break;
                case Ranks.Eight: cardRankValue = "8"; break;
                case Ranks.Seven: cardRankValue = "7"; break;
                case Ranks.Six: cardRankValue = "6"; break;
                case Ranks.Five: cardRankValue = "5"; break;
                case Ranks.Four: cardRankValue = "4"; break;
                case Ranks.Three: cardRankValue = "3"; break;
                case Ranks.Two: cardRankValue = "2"; break;
                default: cardRankValue = card.Rank.ToString(); break;
            }

            return cardRankValue;
        }

    }
}
