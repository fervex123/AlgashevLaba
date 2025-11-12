using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    static class StaticFunc
    {
        public static double InpuOtvet()
        {
            double otvet;
            while (!double.TryParse(Console.ReadLine(), out otvet))
            {
                ConsoleHelper.ConsoleWriteLineColor(false, "Вы ввели не число(");
            }
            return otvet;
        }
        public static double InputGameNumber()
        {
            Console.Clear();
            ConsoleHelper.ConsoleWriteLineColor(true, "Игра угадай число");
            Console.WriteLine("Введите значение А не равное 0:");
            double a;
            while (!double.TryParse(Console.ReadLine(), out a) || a == 0)
            {
                ConsoleHelper.ConsoleWriteLineColor(false, "Введите числовое значение для А не равное 0:");
            }
            return a;
        }
        public static int InputNumber()
        {
            int number;
            while (!int.TryParse(Console.ReadLine(), out number) || number > 5)
            {
                if (number > 5)
                {
                    ConsoleHelper.ConsoleWriteLineColor(false, "Введите число меньше 4");
                }
                else
                {
                    ConsoleHelper.ConsoleWriteLineColor(false, "Введите число!!!!");
                }
            }
            return number;
        }
      
    }
}
