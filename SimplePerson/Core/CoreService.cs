using Simple.CardTable;
using Simple.CardTable.CardTableModel;
using Simple.Testing_Console_UI;

namespace Simple.Core
{
    public class CoreService : ICoreService
    {
        private void GetPlayersFromUserConsole()
        {
            IConsole_UI UI = new Console_UI();

            UI.ShowWellcomeMessage();
            int playersCount = UI.GetCountPlayers();
            int startMoneyAmount = UI.GetStartMoneyAmount();
            List<string> playerNames = UI.GetPlayerNames(playersCount);

            AddPlayersToTable(playerNames, startMoneyAmount);
        }

        private void AddPlayersToTable(List<string> playerNames, int startMoneyAmount)
        {
            foreach (string playerName in playerNames)
            {
                AddNewPlayer(playerName, startMoneyAmount);
            }
        }

        public void AddNewPlayer(string name, decimal startMoney)
        {

            ICardTableService cardTableService = new CardTableService();                                //      Add Interface TableService
            CardPlayer CardPlayerSlava = cardTableService.CreateCardPlayer(name, startMoney);           //      Create Player One

        }

        public void StartGame(int countCardDeks)
        {
            ICardTableService tableService = new CardTableService();

            GetPlayersFromUserConsole();

            while (true)
            {
                ShowNewGame();

                tableService.DealCardsToPlayers(countCardDeks);
                ShowInfoAllPlayers();
            }

        }

        private void ShowNewGame()
        {
            ICardTableService tableService = new CardTableService();
            IConsole_UI UI = new Console_UI();
            UI.Clear();                                                 //      Clear All

            var user = tableService.PlayerController(CardTable.CardTableModel.CardTable.CardPlayers[0]);

        }

        private void ShowInfoAllPlayers()
        {
            Console.Clear();
            IConsole_UI UI = new Console_UI();


            foreach (var cardPlayer in CardTable.CardTableModel.CardTable.CardPlayers)
            {
                UI.ShowCardPlayerInfo(cardPlayer);
            }
        }

    }
}
