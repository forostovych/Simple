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
        void DoActionByUserSelection(UserSelector select, CardPlayer player);

    }
}
