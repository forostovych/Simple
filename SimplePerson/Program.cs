using Simple.Bank;
using Simple.CardTable;
using Simple.CardTable.CardDeckModel;
using Simple.CardTable.CardTableModel;
using Simple.Core;
using Simple.PersonModel.PersonModels;
using Simple.PersonModel.PersonServices;
using Simple.Testing_Console_UI;

namespace Simple
{
    public class Program
    {
        static void Main()
        {

            SimplStartScenario();

            //ICoreService Game = new CoreService();
            //Game.StartGame(3);





        }

        private static void SimplStartScenario()
        {
            ICardTableService cardTableService = new CardTableService();                            //      Add Interface TableService
            IBankService bankService = new BankService();                                           //      Add Interface BankService


            ICardDeckService cardDeckService = new CardDeckService();                               //      Add Interface DeckService


            CardDeck TableCardDeck = cardDeckService.GetCardDeck(1);                                //      Create CardTable Deck
            CardPlayer CardPlayerSlava = cardTableService.CreateCardPlayer("Slava", 75000);         //      Create Player One
            CardPlayer CardPlayerValera = cardTableService.CreateCardPlayer("Valera", 75000);       //      Create Player Two


            for (int i = 0; i < 6; i++)
            {
                (TableCardDeck, CardPlayerSlava.CardDeck) = cardDeckService.MoveCards(TableCardDeck, CardPlayerSlava.CardDeck, 1);
                (TableCardDeck, CardPlayerValera.CardDeck) = cardDeckService.MoveCards(TableCardDeck, CardPlayerValera.CardDeck, 1);
            }



            IConsole_UI UI = new Console_UI();

            UI.ShowCardDeck(TableCardDeck);
            UI.ShowCardPlayerInfo(CardPlayerSlava);
            UI.ShowCardPlayerInfo(CardPlayerValera);

            var transactionStatus = bankService.SendMoney(CardPlayerSlava.Person, CardPlayerValera.Person, 15000);
            UI.ShowTransactionReport(transactionStatus);

            UI.ShowCardPlayerInfo(CardPlayerSlava);
            UI.ShowCardPlayerInfo(CardPlayerValera);
        }
    }

}