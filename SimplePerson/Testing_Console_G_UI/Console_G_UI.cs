namespace Simple.Testing_Console_UI
{
    public class Console_G_UI<Menu> where Menu : Enum
    {
        public Menu GetSelectorFromUser()
        {
            while (true)
            {
                ShowUIMessage(ConvertEnumToUIString());
                string result = Console.ReadLine().ToString();
                if (IsInteger(result) && int.Parse(result) > 0 && int.Parse(result) < Enum.GetValues(typeof(Menu)).Length + 1)
                {
                    Menu resulter = (Menu)Enum.GetValues(typeof(Menu)).GetValue(int.Parse(result) - 1);


                    return resulter;
                }
                ShowUIMessage("Error!");
                Thread.Sleep(500);
            }
        }

        public void Clear()
        {
            Console.Clear();
            Console.ResetColor();
        }


        private bool IsInteger(string value) => int.TryParse(value, out int res);

        private string ConvertEnumToUIString()
        {
            string result = string.Empty;
            int iterator = Enum.GetValues(typeof(Menu)).Length;
            for (int i = 0; i < iterator; i++)
            {
                result += $"{(i + 1)} [{Enum.GetName(typeof(Menu), i)}]\n";
            }

            return result;
        }

        public void ShowUIMessage(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write($"[{text}]");
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

    }
}
