using Simple.GamingTable;
using Simple.GamingTable.CardTableModel;
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
            cardTableService.CreateCardPlayer(name, startMoney);           //      Create Player One

        }

        public void StartGame(int countCardDeks)
        {
            ICardTableService CardTableService = new CardTableService();
            IConsole_UI UI = new Console_UI();
            GetPlayersFromUserConsole();                                    //           Create a game by User Input.  Select Count of players, money Amount fnd PlayerNames

            UI.GetBetFromPlayer(CardTable.CardPlayers[0]);
            CardTableService.DealCardsToPlayers(countCardDeks);             //           Deal Cards too Players
            ShowInfoAllPlayers();                                           //          Show all info

            var userSelection = CardTableService.AskUserSelection();        //          Ask Player about next move.
            //Hit,                //      GetCard
            //Stand,              //      Enough
            //Double,             //      Double bet
            //Surrender           //      Surrender
            UI.ShowUIMessage("[ " + userSelection.ToString() + "]");                     //          Show some info


            CardTableService.DoActionByUserSelection(userSelection, CardTable.CardPlayers[0]);
            ShowInfoAllPlayers();                                         //          Show all info

            //ShowNewGame();

            while (true)
            {


            }

        }

        private void ShowNewGame()
        {
            ICardTableService tableService = new CardTableService();
            IConsole_UI UI = new Console_UI();
            UI.Clear();                                                 //      Clear All

            //var user = tableService.PlayerController(CardTable.CardTableModel.CardTable.CardPlayers[0]);

        }

        private void ShowInfoAllPlayers()
        {
            Console.Clear();
            IConsole_UI UI = new Console_UI();


            foreach (var cardPlayer in GamingTable.CardTableModel.CardTable.CardPlayers)
            {
                UI.ShowCardPlayerInfo(cardPlayer);
            }
        }

    }
}
