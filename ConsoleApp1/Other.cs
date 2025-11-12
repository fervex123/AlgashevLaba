using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    static class Other
    {
        public static bool ExiProgram()
        {
            Console.Clear();
            ConsoleHelper.ConsoleWriteLineColor(true, "Вы дейсвительно хотите выйти: y/n");
            char yn;
            bool menu = true;
            bool vixod = true;
            while (char.TryParse(Console.ReadLine(), out yn) && vixod == true)
            {
                if (yn == 'y' || yn == 'n')
                {
                    if (yn == 'y')
                    {
                        menu = false;
                        vixod = false;
                        Console.WriteLine("нажмите что-то чтобы закрыть");
                    }
                    else
                    {
                        vixod = true;
                        ConsoleHelper.BackToMenuTxt();
                    }
                }
                else
                {
                    ConsoleHelper.ConsoleWriteLineColor(false, "Введите y или n");
                }
            }
            Console.Clear();
            return menu;
        }
        public static void Avtor()
        {
            Console.Clear();
            Console.WriteLine("Вишняков Вячеслав Павлович\nГруппа: 6102-09.03.01");
            ConsoleHelper.BackToMenuTxt();
            Console.ReadKey();
            Console.Clear();
        }
    }
}
