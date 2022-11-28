﻿using Simple.GamingTable;
using Simple.GamingTable.CardTableModel;
using Simple.PersonModel.PersonModels;
using Simple.Testing_Console_UI;
using System.Xml.Linq;

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
            UI.InitializePlayersBet(startMoneyAmount);
            List<string> playerNames = UI.GetPlayerNames(playersCount);

            AddPlayersToTable(playerNames, startMoneyAmount);
        }
        private void AddPlayersToTable(List<string> playerNames, int startMoneyAmount)
        {
            foreach (string playerName in playerNames)
            {
                AddNewPlayer(playerName, startMoneyAmount);
            }
            AddNewDealer("Dealer", startMoneyAmount*10000);
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
            {
                //IConsole_UI UI = new Console_UI();
                //ICardTableService CardTableService = new CardTableService();

                ////ICardTableService.CreateNewGame();

                //CardTableService.RemoveBetFromPlayers();                        //          Take away the first bet
                //CardTableService.DealCardsToPlayers(countCards);                //          Deal Cards to Players
                //ShowInfoAllPlayers();                                           //          Show all info

                //var userSelection = CardTableService.AskUserSelection();        //          Ask Player about next move.
                //UI.ShowUIMessage("[ " + userSelection.ToString() + "]");        //          Show some info
                //CardTableService.DoActionByUserSelection(userSelection, CardTable.CardPlayers[0]);
                //ShowInfoAllPlayers();                                           //          Show all info

                //ShowNewGame();
                //ShowInfoAllPlayers();                               //          Show all info


                ////    ======
                ///
            }

            InitializePlayersFromUserConsole();                                    //          Create a game by User Input.  Select Count of players, money Amount fnd PlayerNames
            ICardTableService CTS = new CardTableService();


            while (true)
            {
                CTS.RemoveBetFromPlayers();                         //          Take away the first bet
                CTS.DealCardsToPlayers(countCards);                 //          Deal Cards to Players
                CTS.AskAllPlayersNextMove(countCards);
                CTS.CountPlayersResult();

                CTS.GameOver();
            }
            ////    ======
        }

        private void ShowNewGame()
        {
            ICardTableService tableService = new CardTableService();
            IConsole_UI UI = new Console_UI();
            UI.Clear();                                                 //      Clear All

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
