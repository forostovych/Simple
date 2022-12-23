using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Testing_Console_UI
{
    public class Console_UI_G<Menu> where Menu : Enum
    {
        public string GetMenuName<Menu>()
        {
            return nameof(Menu);
        }

    }

    public interface IConsole_UI_G
    {
        string GetMenuName<Menu>();
    }
}
