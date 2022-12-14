using Simple;
using Simple.Bank;
using Simple.CardTable.CardDeckModel;
using Simple.CardTable.CardModel;
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
            ICoreService coreService = new CoreService();
            Person elena = coreService.AddPlayer("Elena", PersonRole.Player, 50000);
            Person kitty = coreService.AddPlayer("Kitty", PersonRole.PlayerPro, 50000);
            ShowAllPersonsReport(Data.PersonRepository.entities);
            Console.WriteLine();
            IBankService bankService = new BankService();
            bankService.SendMoney(elena, kitty, 15000);
            ShowAllPersonsReport(Data.PersonRepository.entities);

            ICardDeckService ICardDeck = new CardDeckService();
            IConsole_UI UI = new Console_UI();

            CardDeck cardsDeck_Desk = ICardDeck.GetCardDeck(2);
            CardDeck cardsPlayer = ICardDeck.GetCardDeck(0);

            (cardsDeck_Desk, cardsPlayer) = ICardDeck.MoveCards(cardsDeck_Desk, cardsPlayer, 6);


            UI.ShowCardDeck(cardsDeck_Desk);
            UI.ShowPlayerCardDeck(cardsPlayer, kitty);

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