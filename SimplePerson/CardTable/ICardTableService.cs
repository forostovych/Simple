using Simple.CardTable.CardDeckModel;
using Simple.CardTable.CardTableModel;
using Simple.PersonModel.PersonModels;

namespace Simple.CardTable
{
    public interface ICardTableService
    {
        CardPlayer CreateCardPlayer(string name, decimal amount, PersonRole role = PersonRole.Player);
        void DealCardsToPlayers(int numberOfCads);

        int CalculateCardsWeight(CardDeck cardDeck);
        object PlayerController(CardPlayer cardPlayer);
    }
}
