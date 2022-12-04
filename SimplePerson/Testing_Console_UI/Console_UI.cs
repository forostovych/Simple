using Simple.Bank;
using Simple.Bank.AccountModels;
using Simple.GamingTable;
using Simple.CardTableModel.CardModel;
using Simple.GamingTable.CardDeckModel;
using Simple.GamingTable.CardTableModel;
using Simple.PersonModel.PersonModels;

namespace Simple.Testing_Console_UI
{
    public class Console_UI : IConsole_UI
    {
        public void ShowCardPlayerInfo(CardPlayer cardPlayer)
        {
            ShowPlayerCardDeck(cardPlayer.CardDeck, cardPlayer.Person);
            if (cardPlayer.Person.Role != PersonRole.Dealer)
            {
                ShowCardPlayerAccount(cardPlayer.Account);
            }

            ShowPlayerGameStatus(cardPlayer);
            ShowUIMessage("");
        }

        private void ShowPlayerGameStatus(CardPlayer player)
        {

            ICardTableService TS = new CardTableService();

            string result = TS.GetPlayerGameStatus(player);
            Console.WriteLine($"Game Status by Player:[{player.Person.Name}] [ {result} ]\n");

        }

        private void ShowCardPlayerAccount(Account account)
        {

            IBankService bankService = new BankService();

            Console.Write($"Money Amount: [ ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{bankService.GetMoneyAmount(account)}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" $]");

        }
        public void ShowCardDeck(CardDeck deck)
        {
            foreach (var card in deck.Cards)
            {
                ShowCard(card);
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
        }
        public void ShowPlayerCardDeck(CardDeck deck, Person person)
        {
            ICardDeckService CDS = new CardDeckService();

            ShowPlayerName(person.Name);
            Console.Write("Cards: [ ");
            ShowCardDeckInHand(deck.Cards);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ]");
            var weightOfCards = CDS.CalculateCardsWeight(deck);
            Console.Write($"Card Points: [ ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{(weightOfCards)}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ]");

        }
        private void ShowPlayerName(string name)
        {
            Console.Write($"Player: [ ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{name}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ]");
        }
        private void ShowCardDeckInHand(Queue<Card> cards)
        {
            var Listcards = cards.ToList();

            for (int i = 0; i < Listcards.Count; i++)
            {
                ShowCard(Listcards[i]);
                var memColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                if (i == Listcards.Count - 1) { break; }
                Console.Write("|");
                Console.ForegroundColor = memColor;
            }

        }
        private void ShowCard(Card card)
        {
            Console.ForegroundColor = GetCardSuitColor(card);
            Console.Write($"{GetCardSuitChar(card)}");
            Console.Write($"{GetCardRankValue(card)}");
        }
        private ConsoleColor GetCardSuitColor(Card card)
        {
            switch (card.Suit)
            {
                case Suits.Hearts: return ConsoleColor.DarkRed;
                case Suits.Diamonds: return ConsoleColor.Red;
                case Suits.Spades: return ConsoleColor.DarkGray;
                case Suits.Clubs: return ConsoleColor.DarkCyan;
                //case Suits.Joker: return ConsoleColor.Yellow;
                default: return ConsoleColor.White;
            }
        }
        private char GetCardSuitChar(Card card)
        {
            char cardSuitChar = (char)card.Suit;
            switch (card.Suit)
            {
                case Suits.Hearts: cardSuitChar = '♥'; break;
                case Suits.Diamonds: cardSuitChar = '♦'; break;
                case Suits.Clubs: cardSuitChar = '♣'; break;
                case Suits.Spades: cardSuitChar = '♠'; break;

                default: break;
            }

            return cardSuitChar;
        }
        private string GetCardRankValue(Card card)
        {
            string cardRankValue = string.Empty;
            switch (card.Rank)
            {
                case Ranks.Ten: cardRankValue = "10"; break;
                case Ranks.Nine: cardRankValue = "9"; break;
                case Ranks.Eight: cardRankValue = "8"; break;
                case Ranks.Seven: cardRankValue = "7"; break;
                case Ranks.Six: cardRankValue = "6"; break;
                case Ranks.Five: cardRankValue = "5"; break;
                case Ranks.Four: cardRankValue = "4"; break;
                case Ranks.Three: cardRankValue = "3"; break;
                case Ranks.Two: cardRankValue = "2"; break;
                default: cardRankValue = card.Rank.ToString(); break;
            }

            return cardRankValue;
        }
        public void ShowTransactionReport(string transactionStatus)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('=', 120));
            Console.Write($"Transaction: [");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($" {transactionStatus} ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("]");
            Console.WriteLine(new string('=', 120) + "\n");
            Console.ResetColor();
        }
        public int InitializeCountPlayers()
        {
            int count = 0;
            string inputResult = string.Empty;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Please enter a number of players between 1 and 6: ");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                inputResult = Console.ReadLine();
                if (IsInteger(inputResult) && int.Parse(inputResult) < 6 && int.Parse(inputResult) > 0)
                {
                    count = int.Parse(inputResult);
                    Console.WriteLine($"Number of players is: [ {inputResult} ] - ok.\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erorr!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Please enter a number between 1 and 6: ");
                }
            }

            return count;
        }
        private bool IsInteger(string value) => int.TryParse(value, out int res);
        public List<string> GetPlayerNames(int countPlayers)
        {
            List<string> names = new List<string>();
            for (int i = 0; i < countPlayers; i++)
            {
                names.Add(GetNameFromUser(i + 1));
            }

            return names;
        }
        private string GetNameFromUser(int i)
        {
            while (true)
            {

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"Please enter the name of the {(ConvertNumberToText(i))} player: ");

                Console.ForegroundColor = ConsoleColor.Green;
                string? name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine($"Name of the {(ConvertNumberToText(i))} player: [ {name} ] - ok.\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    return name;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error!!!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

        }
        private string ConvertNumberToText(int i)
        {
            switch (i)
            {
                case 1: return "first";
                case 2: return "second";
                case 3: return "third";
                case 4: return "fourth";
                case 5: return "fifth";
                case 6: return "sixth";
                default: return "";
            }
        }
        public void ShowWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{new string('=', 50)}");
            Console.WriteLine($"Hello!! Welcome to the Black Jack game!!!");
            Console.WriteLine($"{new string('=', 50)}");
            Console.ResetColor();
            Console.WriteLine();
        }
        public int InitializeStartMoneyAmount()
        {
            int count = 0;
            string inputResult = string.Empty;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Please enter a starting money amount: ");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                inputResult = Console.ReadLine();
                if (IsInteger(inputResult) && int.Parse(inputResult) > 1000)
                {
                    count = int.Parse(inputResult);
                    Console.WriteLine($"Starting money amount: [ {inputResult}$ ] - ok.\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erorr!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Please enter a starting money amount: ");
                }
            }

            return count;
        }
        public void Clear()
        {
            Console.Clear();
            Console.ResetColor();
        }
        public void ShowUIMessage(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string('=', 40));
            Console.WriteLine($"{text}");
            Console.WriteLine(new string('=', 40));
            Console.ResetColor();
        }
        public UserSelector GetSelectorFromUser()
        {
            while (true)
            {

                ShowUIMessage(ConvertEnumToUIString());
                string result = Console.ReadLine().ToString();
                if (IsInteger(result) && int.Parse(result) > 0 && int.Parse(result) < Enum.GetValues(typeof(UserSelector)).Length + 1)
                {
                    return (UserSelector)(int.Parse(result) - 1);
                }
                ShowUIMessage("Error!");
                Thread.Sleep(500);

            }
        }

        private string ConvertEnumToUIString()
        {
            string result = string.Empty;
            int iterator = Enum.GetValues(typeof(UserSelector)).Length;
            for (int i = 0; i < iterator - 1; i++)
            {
                result += $"{(i + 1)} [{(UserSelector)i}]\n";
            }

            return result;
        }

        public List<string> InitializePlayerNames(int playersCount)
        {
            throw new NotImplementedException();
        }
        public int InitializePlayersCount()
        {
            int count = 0;
            string inputResult = string.Empty;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Please enter a number of players between 1 and 6: ");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                inputResult = Console.ReadLine();
                if (IsInteger(inputResult) && int.Parse(inputResult) < 6 && int.Parse(inputResult) > 0)
                {
                    count = int.Parse(inputResult);
                    Console.WriteLine($"Number of players is: [ {inputResult} ] - ok.\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erorr!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Please enter a number between 1 and 6: ");
                }
            }

            return count;
        }

        public bool GetFromUserStartNEwOrNo()
        {
            return GetPlayerSelectionNextMove();
        }

        private bool GetPlayerSelectionNextMove()
        {
            IBankService BS = new BankService();
            Console.WriteLine("Start new game?" +
            "\nYes = 1 / No = 2");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                string result = Console.ReadLine();

                if (result == "1")
                    return true;
                if (result == "2")
                    return false;
            }
        }

        public decimal AskPlayerBet(CardPlayer player)
        {
            IBankService BS = new BankService();
            decimal maximumBet = BS.GetMoneyAmount(player.Account);
            string inputResult = string.Empty;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Please enter the bet amount [Maxuimum Bet: {maximumBet}]: ");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                inputResult = Console.ReadLine();
                if (IsInteger(inputResult) && decimal.Parse(inputResult) <= maximumBet)
                {
                    Console.WriteLine($"The bet amount is: [ {inputResult}$ ] - ok.\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erorr!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"Please enter a betting amount between 1$ and {maximumBet}$: ");
                }
            }

            return decimal.Parse(inputResult);
        }
    }
}
