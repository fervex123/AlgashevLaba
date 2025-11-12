using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{ 
 
    public static class StaticFunc
    {
        /// <summary>
        /// Ввод ответа с валидацией
        /// </summary>
        /// <returns>Корректное число double</returns>
        public static double InpuOtvet()
        {
            double otvet;
            while (!double.TryParse(Console.ReadLine(), out otvet))
            {
                ConsoleHelper.ConsoleWriteLineColor(false, "Вы ввели не число(");
            }
            return otvet;
        }
        /// <summary>
        /// Ввод игрового числа с валидацией (не равное 0)
        /// </summary>
        /// <returns>Корректное число double не равное 0</returns>
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
        /// <summary>
        /// Ввод номера меню с валидацией
        /// </summary>
        /// <returns>Корректный номер пункта меню от 1 до 5</returns>
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
