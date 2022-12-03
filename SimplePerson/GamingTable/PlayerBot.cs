using Simple.GamingTable.CardDeckModel;
using Simple.GamingTable.CardTableModel;
using Simple.Testing_Console_UI;

namespace Simple.GamingTable
{
    public class PlayerBot : IPlayerBot
    {

        private readonly CardPlayer Player;
        public PlayerBot(CardPlayer player)
        {
            Player = player;
        }

        public void NextMove()
        {
            ICardTableService CTS = new CardTableService();
            IConsole_UI UI = new Console_UI();
            bool IsNotCompleted = true;

            UI.ShowCardPlayerInfo(Player); //          Show some info.
            Thread.Sleep(1000);

            while (IsNotCompleted)
            {
                UserSelector NextMove = UserSelector.Unknown;
                NextMove = AI_Move();

                IsNotCompleted = CTS.DoActionByUserSelection(NextMove, Player);   //
            }

        }

        private UserSelector AI_Move()
        {
            ICardDeckService CDS = new CardDeckService();

            return CDS.CalculateCardsWeight(Player.CardDeck) < 17 ? UserSelector.Hit : UserSelector.Stand;
        }
    }
}
