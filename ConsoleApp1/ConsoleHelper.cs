using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    static class ConsoleHelper
    {  /// <summary>
       /// Перекраска текста
       /// </summary>
        public static string ConsoleWriteLineColor(bool warning, string a)
        {
            if (warning == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(a);
            Console.ResetColor();
            return a;
        }
        /// <summary>
        /// функция возврата
        /// </summary>
        public static void BackToMenuTxt()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
            Console.ResetColor();
        }
    }
}
