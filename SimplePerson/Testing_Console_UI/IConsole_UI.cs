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
        int GetCountPlayers();
        List<string> GetPlayerNames(int playersCount);
        void ShowWellcomeMessage();
        int GetStartMoneyAmount();
        void Clear();
        void ShowUIMessage(string text);
        UserSelector GetSelectorFromUser();
    }
}
