using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4_ConsoleUI.Resources;
using Connect4_ConsoleUI.UIHelpers;
using Connect4_ConsoleUI.UIProperties;

namespace Connect4_ConsoleUI.Helpers
{
    public static class UserExperience
    {
        public static void PressEnterToContinue()
        {
            Console.WriteLine("Press [Enter] to continue");
            Console.ReadLine();
            Console.Clear();
        }
        public static void PressEnterToContinue(int posX, int posY)
        {
            Print.StringAtPosition("Press [Enter] to continue", posX, posY);
            Console.ReadLine();
            Console.Clear();
        }
    }
}
