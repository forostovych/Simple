using Simple.GamingTable;
using Simple.GamingTable.CardDeckModel;
using Simple.GamingTable.CardTableModel;
using Simple.PersonModel.PersonModels;
using Simple.Testing_Console_UI;

namespace Simple.Core
{
    public class CoreService : ICoreService
    {

        private void InitializePlayersFromUserConsole()
        {
            IConsole_UI UI = new Console_UI();
            UI.ShowWelcomeMessage();
            int playersCount = UI.InitializePlayersCount();
            int startMoneyAmount = UI.InitializeStartMoneyAmount();
            //UI.InitializePlayersBet(startMoneyAmount);
            List<string> playerNames = UI.GetPlayerNames(playersCount);
            AddPlayersToTable(playerNames, startMoneyAmount);
        }

        private void AddPlayersToTable(List<string> playerNames, int startMoneyAmount)
        {
            foreach (string playerName in playerNames)
            {
                AddNewPlayer(playerName, startMoneyAmount);
            }
            AddNewDealer("Dealer", startMoneyAmount * 10000);
        }
        private void AddNewDealer(string name, int startMoney)
        {
            ICardTableService cardTableService = new CardTableService();                                //      Add Interface TableService
            cardTableService.CreateCardPlayer(name, startMoney, PersonRole.Dealer);           //      Create Player One
        }

        public void AddNewPlayer(string name, decimal startMoney)
        {
            ICardTableService cardTableService = new CardTableService();            //      Add Interface TableService
            cardTableService.CreateCardPlayer(name, startMoney);                    //      Create Player One
        }

        public void StartGame(int countCards)
        {
            InitializePlayersFromUserConsole();                                    //          Create a game by User Input.  Select Count of players, money Amount fnd PlayerNames
            ICardTableService CTS = new CardTableService();
            ICardDeckService CDS = new CardDeckService();
            bool isRunning = true;

            CardTable.TableCardDeck = CDS.GetCardDeck(6);

            while (isRunning)
            {
                CTS.RunBlackJackGame();
                CTS.СountPointResult();
                isRunning = CTS.GameOver();
            }
        }

    }
}
