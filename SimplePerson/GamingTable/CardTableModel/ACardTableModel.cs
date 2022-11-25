using Simple.Bank.AccountModels;
using Simple.GamingTable.CardDeckModel;
using Simple.PersonModel.PersonModels;

namespace Simple.GamingTable.CardTableModel
{
    public abstract class ACardTableModel
    {
        public Person Person { get; set; }
        public Account Account { get; set; }
        public CardDeck CardDeck { get; set; }
        public int PlayerBank { get; set; }
        public UserSelector UserSelect { get; set; }
        public GameStatus StatusGame { get; set; }
        public bool MoveIsCompleted { get; set; }
    }
}
