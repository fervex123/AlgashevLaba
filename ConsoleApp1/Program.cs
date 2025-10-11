
using System;
using System.Reflection.Metadata.Ecma335;
class Program 
{ 
    static void Main(string[] args) {

        bool menu = false;
        const double eler = Math.E;
        bool bob= false;
        
        while (menu== false)
        {

            //Console.Clear();
            ConsWriteLnText(true, "Выберите что-то из списка");
            Console.Write("1 - Отгадай ответ\n2 - Об авторе\n4 - Выход\n");
            int number;
          
            while (!int.TryParse(Console.ReadLine(), out number) || number >4)
            {
              
                if (number > 4)
                {
                    ConsWriteLnText(false, "Введите число меньше 3");
                }
                else
                {
                    ConsWriteLnText(false, "Введите число!!!!");
                }
            }
            switch (number)
            {
                case 1:
                    try
                    {
                        Console.Clear();
                        ConsWriteLnText(true, "Игра угадай число");
                        Console.WriteLine("Введите значение А не равное 0:");
                        double a;
                        double otvet;


                        while (!double.TryParse(Console.ReadLine(), out a) || a == 0)
                        {
                            ConsWriteLnText(false, "Введите числовое значение для А не равное 0:");
                        }
                        double verx = (Math.Sin(a) + Math.Tan(2 * a));
                        double niz = (Math.Log(Math.Pow(eler, 2), 3));
                        if (niz < 0)
                        {
                            ConsWriteLnText(false, "Ошибка: попытка извлечь корень из отрицательного числа!");
                            BackToMenuTxt();
                            Console.ReadKey();
                            break;
                        }
                        niz = (Math.Sqrt(niz));
                        double result = verx / niz;
                        result = (Math.Round(result, 2));
                        
                        ConsWriteLnText(true, "Попробуй угадать ответ за 3 попытки с округлением до 2-х знаков после запятой");
                        for (int i = 0; i < 3; i++)
                        {
                            while (!double.TryParse(Console.ReadLine(), out otvet))
                            {
                                ConsWriteLnText(false, "Вы ввели не число(");
                            }
                            Console.WriteLine($"\nПопытка {i + 1} из 3");
                            if (result == otvet)
                            {
                                i = 2;
                               
                                ConsWriteLnText(true, $"Ура победа! Ваш ответ: {otvet}");
                            }
                            if (result != otvet && i == 2)
                            {
                               
                                ConsWriteLnText(false, $"Вы проиграли((( Правильный ответ:{result} ");
                            }
                            if (i != 2)
                            {
                                ConsWriteLnText(false, "Введите ваш ответ (округленный до 2 знаков после запятой):");
                            }
                           
                            
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        ConsWriteLnText(false, $"Ошибка аргумента: {ex.Message}");
                    }
                    catch (DivideByZeroException)
                    {
                        ConsWriteLnText(false, "Деление на 0");
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
    //        ConsWriteLnText(false, "Введите числовое значение для А не равное 0:");
    //    }
    //    double verx = (Math.Sin(a) + Math.Tan(2 * a));
    //    double niz = (Math.Log(Math.Pow(eler, 2), 3));
    //    if (niz < 0)
    //    {
    //        ConsWriteLnText(false, "Ошибка: попытка извлечь корень из отрицательного числа!");
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
    //        ConsWriteLnText(true, "Игра угадай число");
    //        Console.WriteLine("Введите значение А не равное 0:");
    //        double a;
    //        double otvet;


    //        while (!double.TryParse(Console.ReadLine(), out a) || a == 0)
    //        {
    //            ConsWriteLnText(false, "Введите числовое значение для А не равное 0:");
    //        }
    //        double verx = (Math.Sin(a) + Math.Tan(2 * a));
    //        double niz = (Math.Log(Math.Pow(eler, 2), 3));
    //        if (niz < 0)
    //        {
    //            ConsWriteLnText(false, "Ошибка: попытка извлечь корень из отрицательного числа!");
    //            BackToMenuTxt();
    //            Console.ReadKey();
    //            break;
    //        }
    //        niz = (Math.Sqrt(niz));
    //        double result = verx / niz;
    //        result = (Math.Round(result, 2));
            
    //        ConsWriteLnText(true, "Попробуй угадать ответ за 3 попытки с округлением до 2-х знаков после запятой");
    //        for (int i = 0; i < 3; i++)
    //        {
    //            while (!double.TryParse(Console.ReadLine(), out otvet))
    //            {
    //                ConsWriteLnText(false, "Вы ввели не число(");
    //            }
    //            Console.WriteLine($"\nПопытка {i + 1} из 3");
    //            if (i != 2)
    //            {
    //                ConsWriteLnText(false, "Введите ваш ответ (округленный до 2 знаков после запятой):");
    //            }
    //            if (result == otvet)
    //            {
    //                i = 2;
             
    //                Console.WriteLine($"Ура победа! Ваш ответ: {otvet}");
    //            }
    //            if (result != otvet && i == 2)
    //            {
                    
    //                ConsWriteLnText(false, $"Вы проиграли((( Правильный ответ:{result} ");
    //            }
    //        }
    //    }
    //    catch (ArgumentException ex)
    //    {
    //        ConsWriteLnText(false, $"Ошибка аргумента: {ex.Message}");
    //        throw;
    //    }
    //    catch (DivideByZeroException)
    //    {
    //        ConsWriteLnText(false, "Деление на 0");
    //        BackToMenuTxt();
    //        Console.ReadKey();
    //        throw;
    //        break;
    //    }
    //}
    static void BackToMenuTxt()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
        Console.ResetColor();
    }
     public static bool ExiProgram(bool menu)
    {
        Console.Clear();
        ConsWriteLnText(true, "Вы дейсвительно хотите выйти: д|н");
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
                    ConsWriteLnText(false, "Введите д или н");
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
    static string ConsWriteLnText(bool warning, string a)
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



