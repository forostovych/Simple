using Simple.Bank;
using Simple.Bank.AccountModels;
using Simple.CardTable.CardDeckModel;
using Simple.CardTable.CardTableModel;
using Simple.Core;
using Simple.PersonModel.PersonModels;
using Simple.PersonModel.PersonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.CardTable
{
    public class CardTableService : ICardTableService
    {
        public CardPlayer CreateCardPlayer(string name, decimal amount, PersonRole role = PersonRole.Player)
        {
            CardPlayer cardPlayer = new CardPlayer();

            IPersonService personService = new PersonService();
            IBankService bankService = new BankService();
            ICardDeckService ICardDeck = new CardDeckService();


            cardPlayer.Person = personService.CreatePerson(name, role);
            cardPlayer.Account = bankService.CreateAccount(cardPlayer.Person);
            cardPlayer.Person.AccountID = cardPlayer.Account.Id;



            Data.AccountRepository.Add(cardPlayer.Account);
            Data.PersonRepository.Add(cardPlayer.Person);

            bankService.AddMoney(cardPlayer.Person, amount);
            cardPlayer.CardDeck = ICardDeck.GetCardDeck(0);

            return cardPlayer;
        }
    }
}
