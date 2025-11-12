using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{  
    /// <summary>
    /// Статический класс для игры по отгадыванию ответа значения функции
    /// </summary>
    public static class GameOtvet
    {
        /// <summary>
        /// Вычисляет значение функции по заданной формуле
        /// </summary>
        /// <param name="a">Входное значение параметра a</param>
        /// <returns>Результат вычисления функции, округленный до 2 знаков после запятой</returns>
        public static double Formula(double a)
        {
            const double eler = Math.E;
            double verx = (Math.Sin(a) + Math.Tan(2 * a));
            double niz = (Math.Log(Math.Pow(eler, 2), 3));
            if (niz < 0)
            {
                ConsoleHelper.ConsoleWriteLineColor(false, "Ошибка: попытка извлечь корень из отрицательного числа!");
                ConsoleHelper.BackToMenuTxt();
                Console.ReadKey();
            }
            niz = (Math.Sqrt(niz));
            double result = verx / niz;
            result = (Math.Round(result, 2));
            return result;
        }
        /// <summary>
        /// Основной метод игры по отгадыванию значения функции
        /// </summary>
        /// <param name="result">Правильное значение, которое нужно угадать</param>
        public static void Game(double result)
        {
            try
            {
                ConsoleHelper.ConsoleWriteLineColor(true, "Попробуй угадать ответ за 3 попытки с округлением до 2-х знаков после запятой");
                for (int i = 0; i < 3; i++)
                {
                    double otvet = StaticFunc.InpuOtvet();


                    Console.WriteLine($"\nПопытка {i + 1} из 3");
                    if (result == otvet)
                    {
                        i = 2;
                        ConsoleHelper.ConsoleWriteLineColor(true, $"Ура победа! Ваш ответ: {otvet}");
                    }
                    if (result != otvet && i == 2)
                    {
                        ConsoleHelper.ConsoleWriteLineColor(false, $"Вы проиграли((( Правильный ответ:{result} ");
                    }
                    if (i != 2)
                    {
                        ConsoleHelper.ConsoleWriteLineColor(false, "Введите ваш ответ (округленный до 2 знаков после запятой):");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                ConsoleHelper.ConsoleWriteLineColor(false, $"Ошибка аргумента: {ex.Message}");
                throw;
            }
            catch (DivideByZeroException)
            {
                ConsoleHelper.ConsoleWriteLineColor(false, "Деление на 0");
                ConsoleHelper.BackToMenuTxt();
                Console.ReadKey();
                return;
            }
        }
    }
}
