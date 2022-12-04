using Simple.Bank;
using Simple.CardTableModel.CardModel;
using Simple.GamingTable.CardDeckModel;
using Simple.GamingTable.CardTableModel;
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
        public void RemoveBetFromPlayers()
        {
            decimal bet = (decimal)CardTable.DeskBet;
            foreach (CardPlayer player in CardTable.CardPlayers)
            {
                TakeMoneyFromPlayer(player, bet);
            }
        }
        public void AskAllPlayersNextMove(int countCards)
        {
            IConsole_UI UI = new Console_UI();
            ICardTableService CTS = new CardTableService();



            UI.ShowCardPlayerInfo(CardTable.Dealer);
            IPlayerBot DealerBot = new PlayerBot(CardTable.Dealer);
            DealerBot.NextMove();


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
                    СountPointResult();
                }
            }


        }
        public void TakeMoneyFromPlayer(CardPlayer player, decimal amount)
        {
            IBankService BS = new BankService();
            BS.RemoveMoney(player.Person, amount);
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
        public async Task GameOver()
        {
            IConsole_UI UI = new Console_UI();
            UI.Clear();

            UI.ShowUIMessage("Game Over");
            UI.ShowCardPlayerInfo(CardTable.Dealer);

            foreach (var player in CardTable.CardPlayers)
            {
                await UI.ShowCardPlayerInfo(player);
                player.CardDeck.Cards.Clear();
                player.UserSelect = UserSelector.Unknown;
                player.StatusGame = GameStatus.Unknown;
            }
            //Thread.Sleep(3000);
            bool result = UI.GetFromUserStartNEwOrNo();
            CardTable.Dealer.CardDeck.Cards.Clear();

        }
        public void СountPointResult()
        {
            OverPointCheck();
            ComparingPointDealerPlayer();
        }
        private void ComparingPointDealerPlayer()
        {
            ICardDeckService CDS = new CardDeckService();
            CardPlayer Dealer = CardTable.Dealer;
            
            foreach (CardPlayer player in CardTable.CardPlayers)
            {

                if (Dealer.StatusGame == GameStatus.Lose && player.StatusGame != GameStatus.Lose)
                {
                    player.StatusGame = GameStatus.Win;
                    player.StatusGame = GameStatus.Lose;
                }

                if (Dealer.StatusGame == GameStatus.Unknown && player.StatusGame == GameStatus.Lose)
                {
                    Dealer.StatusGame = GameStatus.Win;
                }

                if (Dealer.StatusGame == GameStatus.Unknown && player.StatusGame == GameStatus.Unknown)
                {
                    if (CDS.CalculateCardsWeight(player.CardDeck) > CDS.CalculateCardsWeight(Dealer.CardDeck))
                    {
                        player.StatusGame = GameStatus.Win;
                        Dealer.StatusGame = GameStatus.Lose;
                        continue;
                    }

                    if (CDS.CalculateCardsWeight(player.CardDeck) == CDS.CalculateCardsWeight(Dealer.CardDeck))
                    {
                        player.StatusGame = GameStatus.Draw;
                        Dealer.StatusGame = GameStatus.Draw;
                        continue;
                    }

                    if (CDS.CalculateCardsWeight(player.CardDeck) < CDS.CalculateCardsWeight(Dealer.CardDeck))
                    {
                        player.StatusGame = GameStatus.Lose;
                        Dealer.StatusGame = GameStatus.Win;
                        continue;
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
            IBankService BS = new BankService();
            BS.CreateTransaction(player.Person, CardTable.Dealer.Person, CardTable.DeskBet);
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
            CDS.DealCardToPlayer(player);
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

    }
}
