using Simple.Bank;
using Simple.CardTableModel.CardModel;

namespace Simple.CardTable.CardDeckModel
{
    public class CardDeck : BaseModel
    {
        public Queue<Card> Cards { get; set; }
    }
}
