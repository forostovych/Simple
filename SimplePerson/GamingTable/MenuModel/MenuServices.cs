using Simple.Core;
using Simple.Testing_Console_UI;

namespace Simple.GamingTable.MenuModel
{

    public class MenuServices
    {

        public void MainMenu()
        {

            Console_G_UI<MenuMain> G_UI = new Console_G_UI<MenuMain>();
            G_UI.Clear();
            G_UI.ShowUIMessage(nameof(MainMenu).ToString());
            var currentSelect = G_UI.GetSelectorFromUser();

            switch (currentSelect)
            {
                case MenuMain.SinglePlayer: SinglePlayerMenu(); return;
                case MenuMain.Multiplayer: MultiplayerMenu(); return;
                case MenuMain.Tutorial: TutorialMenu(); return;
                case MenuMain.Options: OptionsMenu(); return;
                case MenuMain.Exit: MenuExit(); return;
            }
        }

        private void SinglePlayerMenu()
        {

            Console_G_UI<MenuSinglePlayer> G_UI = new Console_G_UI<MenuSinglePlayer>();
            G_UI.Clear();
            G_UI.ShowUIMessage(nameof(MenuSinglePlayer).ToString());
            var currentSelect = G_UI.GetSelectorFromUser();

            switch (currentSelect)
            {
                case MenuSinglePlayer.StartGame: SinglePlayerStartGameMenu(); return;
                case MenuSinglePlayer.ExitToMainMenu: MainMenu(); return;
            }

        }

        private void SinglePlayerStartGameMenu()
        {
            ICoreService Game = new CoreService();
            Game.StartGame(2);
        }

        private void MultiplayerMenu()
        {
            Console_G_UI<MenuMultiplayer> G_UI = new Console_G_UI<MenuMultiplayer>();
            G_UI.Clear();
            G_UI.ShowUIMessage(nameof(MenuMultiplayer).ToString());
            var currentSelect = G_UI.GetSelectorFromUser();

            switch (currentSelect)
            {
                case MenuMultiplayer.CreateGame: MultiplayerCreateGame(); return;
                case MenuMultiplayer.FindGame: MultiplayerFindGame(); return;
                case MenuMultiplayer.ExitToMainMenu: MainMenu(); return;
            }
        }

        private void MultiplayerFindGame()
        {
            throw new NotImplementedException();
        }

        private void MultiplayerCreateGame()
        {
            throw new NotImplementedException();
        }

        private void TutorialMenu()
        {
            Console_G_UI<MenuTutorial> G_UI = new Console_G_UI<MenuTutorial>();
            G_UI.Clear();
            G_UI.ShowUIMessage(nameof(MenuTutorial).ToString());
            var currentSelect = G_UI.GetSelectorFromUser();

            switch (currentSelect)
            {
                case MenuTutorial.ExitToMainMenu: MainMenu(); return;
            }
        }
        
        private void OptionsMenu()
        {
            Console_G_UI<MenuOptions> G_UI = new Console_G_UI<MenuOptions>();
            G_UI.Clear();
            G_UI.ShowUIMessage(nameof(MenuOptions).ToString());
            var currentSelect = G_UI.GetSelectorFromUser();

            switch (currentSelect)
            {
                case MenuOptions.ExitToMainMenu : MainMenu(); return;
            }
        }

        private void MenuExit()
        {
            Console_G_UI<MenuMain> G_UI = new Console_G_UI<MenuMain>();
            G_UI.Clear();
            G_UI.ShowUIMessage("Exit");
        }

    }
}
