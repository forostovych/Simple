using Simple.GamingTable;
using Simple.GamingTable.CardTableModel;
using Simple.PersonModel.PersonModels;
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
            UI.GetPlayersBet(startMoneyAmount);
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

        public void StartGame(int countCards)
        {
            IConsole_UI UI = new Console_UI();
            GetPlayersFromUserConsole();                                    //          Create a game by User Input.  Select Count of players, money Amount fnd PlayerNames
            ICardTableService CardTableService = new CardTableService();

            //ICardTableService.CreateNewGame();

            CardTableService.RemoveBetFromPlayers();                          //          Take away the first bet
            CardTableService.DealCardsToPlayers(countCards);             //          Deal Cards too Players
            ShowInfoAllPlayers();                                           //          Show all info

            var userSelection = CardTableService.AskUserSelection();        //          Ask Player about next move.
            UI.ShowUIMessage("[ " + userSelection.ToString() + "]");        //          Show some info
            CardTableService.DoActionByUserSelection(userSelection, CardTable.CardPlayers[0]);
            ShowInfoAllPlayers();                                         //          Show all info

            //ShowNewGame();

            ////    ======
            ICardTableService CTS = new CardTableService();
            while (true)
            {
                CTS.RemoveBetFromPlayers();                         //          Take away the first bet
                CTS.DealCardsToPlayers(countCards);              //          Deal Cards too Players
                ShowInfoAllPlayers();                               //          Show all info
                CTS.AskAllPlayersNextMove(countCards);


            }
            ////    ======
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

            foreach (var cardPlayer in CardTable.CardPlayers)
            {
                UI.ShowCardPlayerInfo(cardPlayer);
            }
        }

    }
}
