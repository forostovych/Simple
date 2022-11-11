using Simple;
using Simple.Bank;
using Simple.CardTable;
using Simple.CardTable.CardDeckModel;
using Simple.CardTable.CardModel;
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
            ICardTableService cardTableService = new CardTableService();
            IBankService bankService = new BankService();
            ICardDeckService cardDeckService = new CardDeckService();
            IConsole_UI UI = new Console_UI();

            CardDeck TableCardDeck = cardDeckService.GetCardDeck(1);

            var CardPlayerIrina = cardTableService.CreateCardPlayer("Irina", 45000);
            var CardPlayerMiroslav = cardTableService.CreateCardPlayer("Miroslav", 45000);

            bankService.SendMoney(CardPlayerIrina.Person, CardPlayerMiroslav.Person, 15000);

            for (int i = 0; i < 6; i++)
            {

                (TableCardDeck, CardPlayerIrina.CardDeck) = cardDeckService.MoveCards(TableCardDeck, CardPlayerIrina.CardDeck, 1);
                (TableCardDeck, CardPlayerMiroslav.CardDeck) = cardDeckService.MoveCards(TableCardDeck, CardPlayerMiroslav.CardDeck, 1);

            }

            UI.ShowCardDeck(TableCardDeck);
            UI.ShowCardPlayerInfo(CardPlayerIrina);
            UI.ShowCardPlayerInfo(CardPlayerMiroslav);


            bankService.SendMoney(CardPlayerIrina.Person, CardPlayerMiroslav.Person, 15000);

            for (int i = 0; i < 6; i++)
            {

                (TableCardDeck, CardPlayerIrina.CardDeck) = cardDeckService.MoveCards(TableCardDeck, CardPlayerIrina.CardDeck, 1);
                (TableCardDeck, CardPlayerMiroslav.CardDeck) = cardDeckService.MoveCards(TableCardDeck, CardPlayerMiroslav.CardDeck, 1);

            }

            UI.ShowCardDeck(TableCardDeck);
            UI.ShowCardPlayerInfo(CardPlayerIrina);
            UI.ShowCardPlayerInfo(CardPlayerMiroslav);
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