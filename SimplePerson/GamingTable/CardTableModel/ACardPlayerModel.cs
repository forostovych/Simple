using Simple.Bank.AccountModels;
using Simple.GamingTable.CardDeckModel;
using Simple.PersonModel.PersonModels;

namespace Simple.GamingTable.CardTableModel
{
    public abstract class ACardPlayerModel
    {

        public Person Person { get; set; }
        public Account Account { get; set; }
        public CardDeck CardDeck { get; set; }
        public decimal Bet {  get; set; }
        public UserSelector UserSelect { get; set; }
        public GameStatus StatusGame { get; set; }
        public bool MoveIsNotCompleted { get; set; }

    }
}
