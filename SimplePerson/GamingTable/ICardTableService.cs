using Simple.GamingTable.CardDeckModel;
using Simple.GamingTable.CardTableModel;
using Simple.PersonModel.PersonModels;

namespace Simple.GamingTable
{
    public interface ICardTableService
    {

        CardPlayer CreateCardPlayer(string name, decimal amount, PersonRole role = PersonRole.Player);
        void DealCardsToPlayers(int numberOfCads);
        int CalculateCardsWeight(CardDeck cardDeck);
        UserSelector AskUserSelection();
        bool DoActionByUserSelection(UserSelector select, CardPlayer player);
        void TakeMoneyFromPlayer(CardPlayer player, decimal amount);
        void RemoveBetFromPlayers();
        void AskAllPlayersNextMove(int countCards);
        void CountPlayersResult();
        string GetPlayerGameStatus(CardPlayer player);
        void GameOver();
    }
}
