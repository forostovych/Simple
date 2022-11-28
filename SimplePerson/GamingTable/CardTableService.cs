using Simple.Bank;
using Simple.GamingTable.CardDeckModel;
using Simple.GamingTable.CardTableModel;
using Simple.CardTableModel.CardModel;
using Simple.PersonModel.PersonModels;
using Simple.PersonModel.PersonServices;
using Simple.Testing_Console_UI;

namespace Simple.GamingTable
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

            if (role != PersonRole.Dealer)
            {
                Data.PersonRepository.Add(cardPlayer.Person);
            }
            Data.AccountRepository.Add(cardPlayer.Account);


            bankService.AddMoney(cardPlayer.Person, amount);
            cardPlayer.CardDeck = ICardDeck.GetCardDeck(0);

            AddPlayerToDesk(cardPlayer);
            return cardPlayer;
        }
        public void DealCardsToPlayers(int numberOfCards)
        {
            ICardDeckService ICardDeck = new CardDeckService();                             //      Add Interface CardDeckService
            var TableCardDeck = CardTable.TableCardDeck = ICardDeck.GetCardDeck(4);

            for (int i = 0; i < numberOfCards; i++)
            {
                foreach (var cardPlayer in CardTable.CardPlayers)
                {
                    (TableCardDeck, cardPlayer.CardDeck) = ICardDeck.MoveCards(TableCardDeck, cardPlayer.CardDeck, 1);
                }

                (TableCardDeck, CardTable.Dealer.CardDeck) = ICardDeck.MoveCards(TableCardDeck, CardTable.Dealer.CardDeck, 1);
            }
        }
        public void RemoveBetFromPlayers()
        {
            decimal bet = (decimal)CardTable.DeskBet;
            foreach (CardPlayer player in CardTable.CardPlayers)
            {
                TakeMoneyFromPlayer(player, bet);
            }
        }
        public void TakeMoneyFromPlayer(CardPlayer player, decimal amount)
        {
            IBankService BS = new BankService();
            BS.RemoveMoney(player.Person, amount);
        }
        public void DealCardToPlayer(CardPlayer cardPlayer)
        {
            ICardDeckService ICardDeck = new CardDeckService();                             //      Add Interface CardDeckService
            (CardTable.TableCardDeck, cardPlayer.CardDeck) = ICardDeck.MoveCards(CardTable.TableCardDeck, cardPlayer.CardDeck, 1);
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
        public UserSelector AskUserSelection()
        {
            IConsole_UI UI = new Console_UI();
            return UI.GetSelectorFromUser();
        }
        public bool DoActionByUserSelection(UserSelector select, CardPlayer player)
        {
            switch (select)
            {
                case UserSelector.Hit: return DoHit(player);                //  true
                case UserSelector.Stand: return DoStand(player);            //  false
                //case UserSelector.Double: DoDouble(player); break;        //  false
                case UserSelector.Surrender: return DoSurrender(player);    //  false

                default: return false;
            }
        }
        public void AskAllPlayersNextMove(int countCards)
        {
            ICardTableService CTS = new CardTableService();
            IConsole_UI UI = new Console_UI();

            for (int i = 0; i < CardTable.CardPlayers.Count; i++)
            {
                bool IsNotCompleted = CardTable.CardPlayers[i].MoveIsCompleted = true;
                while (IsNotCompleted)
                {

                    UI.Clear();
                    UI.ShowCardPlayerInfo(CardTable.CardPlayers[i]); //          Show some info.
                    Thread.Sleep(1000);  
                    if (CardTable.CardPlayers[i].StatusGame == GameStatus.Lose)
                    {
                        break;
                    }

                    CardTable.CardPlayers[i].UserSelect = AskUserSelection(); 
                    IsNotCompleted = DoActionByUserSelection(CardTable.CardPlayers[i].UserSelect, CardTable.CardPlayers[i]);   //
                                                                                                                               //   Do some Action.
                    if (CalculateCardsWeight(CardTable.CardPlayers[i].CardDeck) > 21)
                    {
                        CardTable.CardPlayers[i].StatusGame = GameStatus.Lose;
                        break;
                    }

                }
            }
        }
        private bool DoSurrender(CardPlayer player)
        {
            IBankService BS = new BankService();
            BS.CreateTransaction(CardTable.Dealer.Person, player.Person, CardTable.DeskBet / 2);
            player.StatusGame = GameStatus.Lose;
            return false;
        }
        private bool DoDouble(CardPlayer player)
        {
            throw new NotImplementedException();
        }
        private bool DoStand(CardPlayer player)
        {
            return false;
        }
        private bool DoHit(CardPlayer player)
        {
            DealCardToPlayer(player);
            return true;
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
        private void AddPlayerToDesk(CardPlayer cardPlayer)
        {
            if (cardPlayer.Person.Role == PersonRole.Dealer)
            {
                CardTable.Dealer = cardPlayer;
                return;
            }
            CardTable.CardPlayers.Add(cardPlayer);
        }
        public void CountPlayersResult()
        {

            foreach (var player in CardTable.CardPlayers)
            {
                if (player.StatusGame == GameStatus.Lose)
                {
                    return;
                }

                if (CalculateCardsWeight(player.CardDeck) > CalculateCardsWeight(CardTable.Dealer.CardDeck))
                {
                    ICardTableService CTS = new CardTableService();
                    IBankService IBS = new BankService();


                    player.StatusGame = GameStatus.Win;
                    IBS.CreateTransaction(CardTable.Dealer.Person, player.Person, CardTable.DeskBet * 2);
                }

            }
        }
        public string GetPlayerGameStatus(CardPlayer player) => player.StatusGame.ToString();
        public void GameOver()
        {
            IConsole_UI UI = new Console_UI();
            UI.ShowUIMessage("Game Over");

            foreach (var player in CardTable.CardPlayers)
            {
                UI.ShowCardPlayerInfo(player);
                player.CardDeck.Cards.Clear();
                player.UserSelect = UserSelector.Unknown;
                player.StatusGame = GameStatus.Unknown;
            }
            UI.ShowCardPlayerInfo(CardTable.Dealer);
            Thread.Sleep(3000);
            bool result = UI.GetFromUserStartNEwOrNo();
                 
        }
    }
}
