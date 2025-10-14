
using System;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using static System.Runtime.InteropServices.JavaScript.JSType;
class Program
{
    static void Main(string[] args) {

        bool menu = false;
        const double eler = Math.E;
        bool bob = false;

        while (menu == false)
        {

            //Console.Clear();
            ConsoleWriteLineColor(true, "Выберите что-то из списка");
            Console.Write("1 - Отгадай ответ\n2 - Об авторе\n4 - Выход\n");
            int number;

            while (!int.TryParse(Console.ReadLine(), out number) || number > 4)
            {

                if (number > 4)
                {
                    ConsoleWriteLineColor(false, "Введите число меньше 3");
                }
                else
                {
                    ConsoleWriteLineColor(false, "Введите число!!!!");
                }
            }
            switch (number)
            {
                case 1:
                    try
                    {
                        Console.Clear();
                        ConsoleWriteLineColor(true, "Игра угадай число");
                        Console.WriteLine("Введите значение А не равное 0:");
                        double a;
                        double otvet;


                        while (!double.TryParse(Console.ReadLine(), out a) || a == 0)
                        {
                            ConsoleWriteLineColor(false, "Введите числовое значение для А не равное 0:");
                        }
                        double verx = (Math.Sin(a) + Math.Tan(2 * a));
                        double niz = (Math.Log(Math.Pow(eler, 2), 3));
                        if (niz < 0)
                        {
                            ConsoleWriteLineColor(false, "Ошибка: попытка извлечь корень из отрицательного числа!");
                            BackToMenuTxt();
                            Console.ReadKey();
                            break;
                        }
                        niz = (Math.Sqrt(niz));
                        double result = verx / niz;
                        result = (Math.Round(result, 2));

                        ConsoleWriteLineColor(true, "Попробуй угадать ответ за 3 попытки с округлением до 2-х знаков после запятой");
                        for (int i = 0; i < 3; i++)
                        {
                            while (!double.TryParse(Console.ReadLine(), out otvet))
                            {
                                ConsoleWriteLineColor(false, "Вы ввели не число(");
                            }
                            Console.WriteLine($"\nПопытка {i + 1} из 3");
                            if (result == otvet)
                            {
                                i = 2;

                                ConsoleWriteLineColor(true, $"Ура победа! Ваш ответ: {otvet}");
                            }
                            if (result != otvet && i == 2)
                            {

                                ConsoleWriteLineColor(false, $"Вы проиграли((( Правильный ответ:{result} ");
                            }
                            if (i != 2)
                            {
                                ConsoleWriteLineColor(false, "Введите ваш ответ (округленный до 2 знаков после запятой):");
                            }


                        }
                    }
                    catch (ArgumentException ex)
                    {
                        ConsoleWriteLineColor(false, $"Ошибка аргумента: {ex.Message}");
                    }
                    catch (DivideByZeroException)
                    {
                        ConsoleWriteLineColor(false, "Деление на 0");
                        BackToMenuTxt();
                        Console.ReadKey();
                        break;
                    }
                    BackToMenuTxt();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 2:
                    Avtor();
                    break;
                case 3:
                    Console.Clear();
                    WriteArray();

                    break;
                case 4:
                    ExiProgram(bob);
                    menu = bob;
                    break;

            }
        }
    }
    //static double Formula(double kley)
    //{
    //    const double eler = Math.E;
    //    double a;
    //    double otvet;
    //    while (!double.TryParse(Console.ReadLine(), out a) || a == 0)
    //    {
    //        ConsoleWriteLineColor(false, "Введите числовое значение для А не равное 0:");
    //    }
    //    double verx = (Math.Sin(a) + Math.Tan(2 * a));
    //    double niz = (Math.Log(Math.Pow(eler, 2), 3));
    //    if (niz < 0)
    //    {
    //        ConsoleWriteLineColor(false, "Ошибка: попытка извлечь корень из отрицательного числа!");
    //        BackToMenuTxt();
    //        Console.ReadKey();
    //        break;
    //    }

    //    niz = (Math.Sqrt(niz));
    //    double result = verx / niz;
    //    result = (Math.Round(result, 2));
    //    return result;
    //}
    //private static void Game()
    //{
    //    const double eler = Math.E;
    //    try
    //    {
    //        Console.Clear();
    //        ConsoleWriteLineColor(true, "Игра угадай число");
    //        Console.WriteLine("Введите значение А не равное 0:");
    //        double a;
    //        double otvet;


    //        while (!double.TryParse(Console.ReadLine(), out a) || a == 0)
    //        {
    //            ConsoleWriteLineColor(false, "Введите числовое значение для А не равное 0:");
    //        }
    //        double verx = (Math.Sin(a) + Math.Tan(2 * a));
    //        double niz = (Math.Log(Math.Pow(eler, 2), 3));
    //        if (niz < 0)
    //        {
    //            ConsoleWriteLineColor(false, "Ошибка: попытка извлечь корень из отрицательного числа!");
    //            BackToMenuTxt();
    //            Console.ReadKey();
    //            break;
    //        }
    //        niz = (Math.Sqrt(niz));
    //        double result = verx / niz;
    //        result = (Math.Round(result, 2));

    //        ConsoleWriteLineColor(true, "Попробуй угадать ответ за 3 попытки с округлением до 2-х знаков после запятой");
    //        for (int i = 0; i < 3; i++)
    //        {
    //            while (!double.TryParse(Console.ReadLine(), out otvet))
    //            {
    //                ConsoleWriteLineColor(false, "Вы ввели не число(");
    //            }
    //            Console.WriteLine($"\nПопытка {i + 1} из 3");
    //            if (i != 2)
    //            {
    //                ConsoleWriteLineColor(false, "Введите ваш ответ (округленный до 2 знаков после запятой):");
    //            }
    //            if (result == otvet)
    //            {
    //                i = 2;

    //                Console.WriteLine($"Ура победа! Ваш ответ: {otvet}");
    //            }
    //            if (result != otvet && i == 2)
    //            {

    //                ConsoleWriteLineColor(false, $"Вы проиграли((( Правильный ответ:{result} ");
    //            }
    //        }
    //    }
    //    catch (ArgumentException ex)
    //    {
    //        ConsoleWriteLineColor(false, $"Ошибка аргумента: {ex.Message}");
    //        throw;
    //    }
    //    catch (DivideByZeroException)
    //    {
    //        ConsoleWriteLineColor(false, "Деление на 0");
    //        BackToMenuTxt();
    //        Console.ReadKey();
    //        throw;
    //        break;
    //    }
    //}

    static int ArrayLengthMethod()
    {
        int arrayLng;
        ConsoleWriteLineColor(true, "Введите длину массива не длинее 10");
        while (!int.TryParse(Console.ReadLine(), out arrayLng) || arrayLng < 1)
        {
            ConsoleWriteLineColor(false, "Введите другую длину массива");
        }
        return arrayLng;
    }
    static int[] RandomArray()
    {

        int size = ArrayLengthMethod();
        int[] a = new int[size];
        Random rnd = new Random();
        for (int i = 0; i < a.Length; i++)
        {

            a[i] = rnd.Next(-10, 10);
        }

        return a;
    }
    static int[] CopyArr(int[] originalArray)
    {
        int[] copyArray = new int[originalArray.Length];
        for (int i = 0; i < originalArray.Length; i++)
        {
            copyArray[i] = originalArray[i];
        }
        return copyArray;
    }
   
    static void VivodVseh(int[] allArr)
    {

        foreach (int num in allArr)
        {
            Console.Write(num + " ");
        }
        Console.WriteLine("\n");

    }
    static void WriteArray()
    {
        int[] myArray = RandomArray();
        if (myArray.Length > 10 == false)
        {
            Console.WriteLine("Сгенерированный массив:");
            VivodVseh(myArray);
            Console.WriteLine("\n");

            int[] copyArr = CopyArr(myArray);
            Console.WriteLine("Скопированный массив:");
            VivodVseh(copyArr);
            Console.WriteLine("\n");
            Console.WriteLine("Сортировка пузырьком:");
            int[] sortbubble = SortBubble(myArray);
            VivodVseh(sortbubble);

            Console.WriteLine("Сортировка выбором:");
            int[] min  = SortSelection(copyArr);
            VivodVseh(min);
        }
        else
        {
            ConsoleWriteLineColor(false, "Массив не возможно вывести так как больше 10");
        }
        BackToMenuTxt();
        Console.ReadKey();
        Console.Clear();
    }
    static int[] SortBubble(int[] original)
    {
        //Console.WriteLine("sort bubble");

        int[] sortArray = new int[original.Length];
        for (int i = 0; i < original.Length; i++)
        {
            sortArray[i] = original[i];
        }

        int n = sortArray.Length;

        int swap;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            { 
                Console.ForegroundColor = ConsoleColor.Blue;
                if (sortArray[j] > sortArray[j + 1])
                {   
                    swap = sortArray[j];
                    sortArray[j] = sortArray[j + 1];
                    sortArray[j + 1] = swap;
                }
                Console.ResetColor();
            }
        }
        return sortArray;
    }
    static int[] SortSelection(int[] copy)
    {
        int[] sortArray = new int[copy.Length];
        int n = copy.Length;
        for (int i = 0; i < copy.Length; i++)
        {
            sortArray[i] = copy[i];
        }
        for (int i = 0; i < n - 1; i++)
        {
            int min = i;
            for (int j = i + 1; j < n; j++)
            {
                if (copy[j] < copy[min])
                {
                    min = j;
                }
            }
            int temp = copy[min];
            copy[min] = copy[i];
            copy[i] = temp;
        }
        return copy;
    }


    static void BackToMenuTxt()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
        Console.ResetColor();
    }
     public static bool ExiProgram(bool menu)
    {
        Console.Clear();
        ConsoleWriteLineColor(true, "Вы дейсвительно хотите выйти: д|н");
        char yn;
        menu = false;
        bool vixod = false;
        while (vixod == false)
        {
            while (char.TryParse(Console.ReadLine(), out yn))
            {
                if (yn == 'д' || yn == 'н')
                {
                    if (yn == 'д')
                    {
                        menu = true;
                        vixod = true;
                        Console.WriteLine("нажмите что-то чтобы закрыть");
                        Console.ReadKey();
                    }
                    else
                    {
                        vixod = true;
                        BackToMenuTxt();
                    }
                }
                else
                {
                    ConsoleWriteLineColor(false, "Введите д или н");
                }
            }
        }
        Console.Clear();
        return menu;
    }
    static void Avtor()
    {
        Console.Clear();
        Console.WriteLine("Вишняков Вячеслав Павлович\nГруппа: 6102-09.03.01");
        BackToMenuTxt();
        Console.ReadKey();
        Console.Clear();
    }
    static string ConsoleWriteLineColor(bool warning, string a)
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
}



