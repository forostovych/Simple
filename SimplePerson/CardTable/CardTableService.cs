using Simple.Bank;
using Simple.CardTable.CardDeckModel;
using Simple.CardTable.CardTableModel;
using Simple.CardTableModel.CardModel;
using Simple.PersonModel.PersonModels;
using Simple.PersonModel.PersonServices;
using Simple.Testing_Console_UI;
using System.Net.Http.Headers;

namespace Simple.CardTable
{
    public class CardTableService : ICardTableService
    {
        public CardPlayer CreateCardPlayer(string name, decimal amount, PersonRole role = PersonRole.Player)
        {
            CardPlayer cardPlayer = new CardPlayer();

            IPersonService personService = new PersonService();                             //      Add Interface PersonService
            IBankService bankService = new BankService();                                   //      Add Interface BankService
            ICardDeckService ICardDeck = new CardDeckService();                             //      Add Interface CardDeckService

            cardPlayer.Person = personService.CreatePerson(name, role);                     //      
            cardPlayer.Account = bankService.CreateAccount(cardPlayer.Person);              //      
            cardPlayer.Person.AccountID = cardPlayer.Account.Id;                            //      

            Data.AccountRepository.Add(cardPlayer.Account);
            Data.PersonRepository.Add(cardPlayer.Person);

            bankService.AddMoney(cardPlayer.Person, amount);
            cardPlayer.CardDeck = ICardDeck.GetCardDeck(0);

            AddPlayerToDesk(cardPlayer);
            return cardPlayer;
        }
        private void AddPlayerToDesk(CardPlayer cardPlayer)
        {
            CardTableModel.CardTable.CardPlayers.Add(cardPlayer);
        }
        public void DealCardsToPlayers(int numberOfCads)
        {
            ICardDeckService ICardDeck = new CardDeckService();                             //      Add Interface CardDeckService
            var TableCardDeck = CardTableModel.CardTable.TableCardDeck = ICardDeck.GetCardDeck(4);

            for (int i = 0; i < numberOfCads; i++)
            {
                foreach (var cardPlayer in CardTableModel.CardTable.CardPlayers)
                {
                    (TableCardDeck, cardPlayer.CardDeck) = ICardDeck.MoveCards(TableCardDeck, cardPlayer.CardDeck, 1);
                }
            }

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

        public UserSelector AskUserSelection(UserSelector userSelector)
        {
            IConsole_UI UI = new Console_UI();
            return UI.GetSelectorFromUser();
        }
    }
}
