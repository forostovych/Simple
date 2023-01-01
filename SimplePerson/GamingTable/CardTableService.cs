using Simple.Bank;
using Simple.Bank.AccountModels;
using Simple.CardTableModel.CardModel;
using Simple.GamingTable.CardDeckModel;
using Simple.GamingTable.CardTableModel;
using Simple.PersonModel.PersonModels;
using Simple.PersonModel.PersonServices;
using Simple.Testing_Console_UI;
using System.Numerics;

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
                case UserSelector.Double: return DoDouble(player);          //  false
                case UserSelector.Surrender: return DoSurrender(player);    //  false

                default: return false;
            }
        }
        public string GetPlayerGameStatus(CardPlayer player) => player.StatusGame.ToString();

        public bool GameOver()
        {
            IConsole_UI UI = new Console_UI();
            UI.Clear();

            UI.ShowUIMessage("Game Over");
            UI.ShowCardPlayerInfo(CardTable.Dealer);

            foreach (var player in CardTable.CardPlayers)
            {

                UI.ShowCardPlayerInfo(player);
                player.CardDeck.Cards.Clear();
                player.UserSelect = UserSelector.Unknown;
                player.StatusGame = GameStatus.Unknown;

                if (!IsPlayerMoneyExist(player))
                {
                    return false;
                }
                 
            }


            //Thread.Sleep(3000);
            bool result = UI.GetFromUserStartNEwOrNo();
            CardTable.Dealer.CardDeck.Cards.Clear();
            CardTable.Dealer.StatusGame = GameStatus.Unknown;
            return result;
        }

        public void СountPointResult()
        {
            OverPointCheck();
            ComparingPointDealerPlayer();
        }

        private bool IsPlayerMoneyExist(CardPlayer player)
        {
            IBankService BS = new BankService();
            if (BS.GetMoneyAmountByPerson(player.Person) < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ComparingPointDealerPlayer()
        {
            ICardDeckService CDS = new CardDeckService();
            CardPlayer Dealer = CardTable.Dealer;
            
            foreach (CardPlayer player in CardTable.CardPlayers)
            {

                if (Dealer.StatusGame == GameStatus.Lose && player.StatusGame == GameStatus.Unknown)
                {
                    player.StatusGame = GameStatus.Win;
                }

                if (player.StatusGame == GameStatus.Unknown)
                {
                    if (CDS.CalculateCardsWeight(player.CardDeck) > CDS.CalculateCardsWeight(Dealer.CardDeck))
                    {
                        player.StatusGame = GameStatus.Win;
                        continue;
                    }
                    if (CDS.CalculateCardsWeight(player.CardDeck) == CDS.CalculateCardsWeight(Dealer.CardDeck))
                    {
                        player.StatusGame = GameStatus.Draw;
                        IBankService BS = new BankService();
                        BS.CreateTransaction(CardTable.Dealer.Person, player.Person, player.Bet);
                        continue;
                    }
                    if (CDS.CalculateCardsWeight(player.CardDeck) < CDS.CalculateCardsWeight(Dealer.CardDeck))
                    {
                        player.StatusGame = GameStatus.Lose;
                        continue;
                    }
                }

            }
        }

        private bool DoSurrender(CardPlayer player)
        {
            IBankService BS = new BankService();
            BS.CreateTransaction(CardTable.Dealer.Person, player.Person, player.Bet / 2);
            player.StatusGame = GameStatus.Lose;
            return false;
        }

        private bool DoDouble(CardPlayer player)
        {
            IBankService BS = new BankService();
            BS.CreateTransaction(player.Person, CardTable.Dealer.Person, player.Bet);
            DoHit(player);
            return false;
        }

        private bool DoStand(CardPlayer player)
        {
            return false;
        }

        private bool DoHit(CardPlayer player)
        {
            ICardDeckService CDS = new CardDeckService();
            CDS.DealCardToPlayer(player, 1);
            return true;
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

        private void OverPointCheck()
        {
            PlayersOverPointCheck();
            DealerOverPointCheck();

        }

        private void PlayersOverPointCheck()
        {
            ICardDeckService CDS = new CardDeckService();

            foreach (CardPlayer player in CardTable.CardPlayers)
            {
                bool IsOverPoint = CDS.BlackJackOverPointCheck(player.CardDeck);
                if (IsOverPoint)
                {
                    player.StatusGame = GameStatus.Lose;
                }
            }
        }

        private void DealerOverPointCheck()
        {
            ICardDeckService CDS = new CardDeckService();

            bool IsOverPoint = CDS.BlackJackOverPointCheck(CardTable.Dealer.CardDeck);
            if (IsOverPoint)
            {
                CardTable.Dealer.StatusGame = GameStatus.Lose;
            }

        }

        private void BlackJackCheck(CardPlayer player)
        {
            ICardDeckService CDS = new CardDeckService();
            if (CDS.BlackJackCheck(player) && player.CardDeck.Cards.Count == 2)
            {
                player.StatusGame = GameStatus.BlackJack;
            }
        }
        public void RunBlackJackGame()
        {
            ICardDeckService CDS = new CardDeckService();
            foreach (CardPlayer player in CardTable.CardPlayers)
            {
                AskPlayerBet(player);
                RemoveBetFromPlayer(player);
                CDS.DealCardToPlayer(player, 2);
                AskPlayerNextMove(player);
            }

            IPlayerBot DealerBot = new PlayerBot(CardTable.Dealer);
            DealerBot.NextMove();
        }
        private void AskPlayerNextMove(CardPlayer player)
        {
            IConsole_UI UI = new Console_UI();
            bool IsNotCompleted = player.MoveIsNotCompleted = true;
            while (IsNotCompleted)
            {
                UI.Clear();
                UI.ShowCardPlayerInfo(player); //          Show some info.
                BlackJackCheck(player);

                if (player.StatusGame == GameStatus.BlackJack)
                {
                    IsNotCompleted = false;
                    continue;
                }

                player.UserSelect = AskUserSelection();
                IsNotCompleted = DoActionByUserSelection(player.UserSelect, player);   //

                PlayersOverPointCheck();

                if (player.StatusGame == GameStatus.Lose)
                {
                    IsNotCompleted = false;
                }
            }
        }
        private void AskPlayerBet(CardPlayer player)
        {
            IConsole_UI UI = new Console_UI();
            player.Bet = UI.AskPlayerBet(player);
        }
        private void RemoveBetFromPlayer(CardPlayer player)
        {
            IBankService BS = new BankService();
            BS.RemoveMoney(player.Person, player.Bet);
        }

        public void DuMoneyPay()
        {
            IBankService BS = new BankService();
            IConsole_UI UI = new Console_UI();

            string result = string.Empty;

            foreach (CardPlayer player in CardTable.CardPlayers)
            {
                if (player.StatusGame == GameStatus.Win)
                {
                    result = BS.SendMoney(CardTable.Dealer.Person, player.Person, player.Bet * 2);
                    UI.ShowUIMessage(result);
                }

                if (player.StatusGame == GameStatus.BlackJack)
                {
                    result = BS.SendMoney(CardTable.Dealer.Person, player.Person, player.Bet * 3);
                    UI.ShowUIMessage(result);
                }
            }

        }
    }
}
