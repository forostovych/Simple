using Simple.GamingTable.CardDeckModel;
using Simple.GamingTable.CardTableModel;
using Simple.PersonModel.PersonModels;

namespace Simple.Testing_Console_UI
{
    public interface IConsole_UI
    {
        void ShowCardPlayerInfo(CardPlayer cardPlayer);
        void ShowCardDeck(CardDeck deck);
        public void ShowPlayerCardDeck(CardDeck deck, Person person);
        void ShowTransactionReport(string transactionStatus);
        int InitializeCountPlayers();
        List<string> InitializePlayerNames(int playersCount);
        int InitializeStartMoneyAmount();
        void Clear();
        void ShowUIMessage(string text);
        UserSelector GetSelectorFromUser();
        void InitializePlayersBet(int startAmount);
        void ShowWelcomeMessage();
        int InitializePlayersCount();
        List<string> GetPlayerNames(int playersCount);
        bool GetFromUserStartNEwOrNo();
    }
}
