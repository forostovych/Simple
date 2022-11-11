using Simple.Bank;
using Simple.CardTable;
using Simple.CardTable.CardDeckModel;
using Simple.PersonModel.PersonModels;
using Simple.PersonModel.PersonServices;
using Simple.Testing_Console_UI;

namespace Simple
{
    public class Program
    {
        static void Main()
        {

            ICardTableService cardTableService = new CardTableService();
            IBankService bankService = new BankService();
            ICardDeckService cardDeckService = new CardDeckService();

            CardDeck TableCardDeck = cardDeckService.GetCardDeck(1);

            var CardPlayerSlava = cardTableService.CreateCardPlayer("Slava", 10000);
            var CardPlayerValera = cardTableService.CreateCardPlayer("Valera", 45000);


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

        static void ShowAllPersonsReport(List<Person> peoples)
        {
            PersonService personService = new PersonService();
            foreach (Person person in peoples)
            {
                Console.WriteLine(personService.GetPersonReport(person));
            }
        }

    }
}