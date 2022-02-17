using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4_ConsoleUI.Helpers
{
    public static class StringHelper
    {
        public static string GetLongestListItem(List<string> list)
        {
            var longestItem = "";
            foreach (var item in list)
            {
                if (item.Length > longestItem.Length)
                {
                    longestItem = item;
                }
            }
            return longestItem;
        }
    }
}
