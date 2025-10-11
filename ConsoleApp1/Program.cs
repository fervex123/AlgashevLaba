
using System;
using System.Reflection.Metadata.Ecma335;
class Program
{
    static void Main(string[] args) {

        bool menu = true;
        const double eler = Math.E;
        while (menu)
        {
            Console.Clear();
            Console.WriteLine("Выберите что-то из списка");
            Console.Write("1 - Отгадай ответ\n2 - Об авторе\n3 - Выход\n");
            int number;


            //bool boba = !int.TryParse(Console.ReadLine(), out number);

            //while (boba)
            
          
            while (int.TryParse(Console.ReadLine(), out number)== false)
            {
              
                if (number > 3)
                {

                    Console.WriteLine("Введите число меньше 3!");
                  
                }
                else
                {

                    Console.WriteLine("Введите число!!!!");
                }
            }
            switch (number)
            {
                case 1:
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("Игра угадай число");
                        Console.WriteLine("Введите значение А не равное 0:");
                        double a;
                        double otvet;
                        while (!double.TryParse(Console.ReadLine(), out a) || a == 0)
                        {

                            Console.WriteLine("Введите числовое значение для А не равное 0:");
                        }
                        double verx = (Math.Sin(a) + Math.Tan(2 * a));
                        double niz = (Math.Log(Math.Pow(eler, 2), 3));
                        if (niz < 0)
                        {
                            Console.WriteLine("Ошибка: попытка извлечь корень из отрицательного числа!");
                            Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
                            Console.ReadKey();
                            break;
                        }
                        niz = (Math.Sqrt(niz));
                        double result = verx / niz;
                        result = (Math.Round(result, 2));
                        bool game = false;
                        Console.WriteLine("Попробуй угадать ответ за 3 попытки с округлением до 2-х знаков после запятой");
                        for (int i = 0; i < 3; i++)
                        {
                            while (!double.TryParse(Console.ReadLine(), out otvet))
                            {
                                Console.WriteLine("Вы ввели не число(");
                            }
                            Console.WriteLine($"\nПопытка {i + 1} из 3");
                            if (i != 2)
                            {
                                Console.WriteLine("Введите ваш ответ (округленный до 2 знаков после запятой):");
                            }
                            if (result == otvet)
                            {
                                i = 2;
                                game = true;
                                Console.WriteLine($"Ура победа! Ваш ответ: {otvet}");
                            }
                            if (result != otvet && i == 2)
                            {
                                game = false;
                                Console.WriteLine($"Вы проиграли((( Правильный ответ:{result} ");
                            }
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Ошибка аргумента: {ex.Message}");
                    }
                    catch (DivideByZeroException)
                    {
                        Console.WriteLine("Деление на 0");
                        Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
                        Console.ReadKey();
                        break;
                    }
                    Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Вишняков Вячеслав Павлович\nГруппа: 6102-09.03.01");
                    Console.WriteLine("Для выхода в меню нажмите любую кнопку");
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Вы дейсвительно хотите выйти: д|н");
                    char yn;
                    bool vixod = false;
                    while (!vixod)
                    {
                        while (char.TryParse(Console.ReadLine(), out yn))
                        {
                            if (yn == 'д' || yn == 'н')
                            {
                                if (yn == 'д')
                                {
                                    menu = false;
                                    vixod = true;
                                    Console.WriteLine("нажмите что-то чтобы закрыть");
                                    Console.ReadKey();
                                    
                                }
                                else
                                {
                                    vixod = true;
                                    Console.WriteLine("Нажмите любую кнопку чтобы вернутся в меню");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Введите д или н");
                            }
                        }   
                    }
                break;
            }
        } 
    }
}



