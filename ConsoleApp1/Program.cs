
using System;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static bool[,] mines;
    static char[,] playerView;
    static int openedCells = 0;
    static bool gameOver = false;


    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool bMaximumWindow, ref CONSOLE_FONT_INFO_EX lpConsoleCurrentFontEx);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CONSOLE_FONT_INFO_EX
    {
        public uint cbSize;
        public uint nFont;
        public COORD dwFontSize;
        public int FontFamily;
        public int FontWeight;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FaceName;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COORD
    {
        public short X;
        public short Y;
    }

    private const int STD_OUTPUT_HANDLE = -11;
    private const int TMPF_TRUETYPE = 4;
    private const int LF_FACESIZE = 32;

    static void SetConsoleFontSize(short fontSize)
    {
        IntPtr hnd = GetStdHandle(STD_OUTPUT_HANDLE);
        if (hnd != IntPtr.Zero)
        {
            CONSOLE_FONT_INFO_EX info = new CONSOLE_FONT_INFO_EX();
            info.cbSize = (uint)Marshal.SizeOf(info);
            info.FontFamily = TMPF_TRUETYPE;
            info.FaceName = "Consolas"; // Можно изменить на другой шрифт
            info.dwFontSize = new COORD { X = 0, Y = fontSize };
            info.FontWeight = 400; // Normal weight

            SetCurrentConsoleFontEx(hnd, false, ref info);
        }
    }






    static void Main(string[] args) {

        SetConsoleFontSize(24); 
        


        bool menu = true;
        while (menu == true)
        {
            ConsoleWriteLineColor(true, "Выберите что-то из списка");
            Console.Write("1 - Отгадай ответ\n2 - Об авторе\n3 - Сортировка массива \n4 - Сапер\n5 - Выход\n");
            int number= InputNumber();
            switch (number)
            {
                case 1:
                    double a = InputGameNumber();
                    double result = Formula(a);
                    Game(result);
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
                    Console.Clear();
                    PlayGame();




                    break;
                case 5:
                    menu = ExiProgram();
                    break;
            }
        }
    }
    static void PlayGame()
    {
        GenerateMines();
        InitializePlayerView();

        bool gameRunning = true;

        while (gameRunning)
        {
         
            Console.Clear();
            Console.WriteLine("=== ИГРА САПЁР ===");
            DisplayBoard();

            // Получаем ход игрока
            (int row, int col) = GetPlayerInput();

            // Обрабатываем ход
            OpenCell(row, col);

            //// TODO: Здесь позже добавим проверку победы/поражения
            
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
    static void InitializePlayerView()
    {
        playerView = new char[5, 5];

        // Заполняем все клетки точками (неоткрытые)
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                playerView[i, j] = '.';
            }
        }
    }
    static void GenerateMines()
    {
        mines = new bool[5, 5];
        Random rand = new Random();
        int onesCount = 0;

        // false
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                mines[i, j] = false;
            }
        }

        // Добавляем true
        while (onesCount < 5)
        {
            int i = rand.Next(0, 5);
            int j = rand.Next(0, 5);

            if (mines[i, j] == false)
            {
                mines[i, j] = true;
                onesCount++;
            }
        }
    }




    static (int, int) GetPlayerInput()
    {
        while (true)
        {
            Console.Write("Введите координаты (например: A1): ");
            string coordinat = Console.ReadLine().ToUpper();
            if (coordinat == "")
            {
                Console.WriteLine("Координаты не могут быть пустыми");
                continue;
            }
            if (coordinat.Length == 2 && char.IsLetter(coordinat[0]) && char.IsDigit(coordinat[1]))
            {
                int col = coordinat[0] - 'A';
                int row = coordinat[1] - '1';

                if (row >= 0 && row < 5 && col >= 0 && col < 5)
                {
                    return (row, col);
                }
                else
                {
                    Console.WriteLine("Ошибка: Координаты должны быть от A1 до E5!");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: Неправильный формат! Используйте букву и цифру (например: A1)");
            }
        }
        
    }
    static void OpenCell(int row, int col)
    {
        // Проверяем, не открыта ли уже клетка
        if (playerView[row, col] != '.')
        {
            Console.WriteLine("Эта клетка уже открыта!");
            return;
        }

        // Если наступили на мину

        if (mines[row, col])
        {
            playerView[row, col] = '*';
            Console.WriteLine(" БОМБА! Вы проиграли!");
            gameOver = true;
            return;
        }
        // Подсчитываем мины вокруг
        int mineCount = CountAdjacentMines(row, col);

        if (mineCount > 0)
        {
            // Показываем цифру
            playerView[row, col] = char.Parse(mineCount.ToString());
        }
        else
        {
            // Если мин вокруг нет, открываем клетку как пустую
            playerView[row, col] = ' ';
  
            OpenAdjacentCells(row, col);
        }
        if (openedCells == 20)
        {
            gameOver = true;
        }
    }
    static void OpenAdjacentCells(int row, int col)
    {
        // Проверяем все 8 соседних клеток
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int newRow = row + i;
                int newCol = col + j;

                // Пропускаем текущую клетку и проверяем границы
                if ((i == 0 && j == 0) ||
                    newRow < 0 || newRow >= 5 ||
                    newCol < 0 || newCol >= 5)
                {
                    continue;
                }

                // Если клетка еще не открыта И не мина
                if (playerView[newRow, newCol] == '.' && !mines[newRow, newCol])
                {
                    int adjacentMines = CountAdjacentMines(newRow, newCol);

                    if (adjacentMines > 0)
                    {
                        // Открываем с цифрой
                        playerView[newRow, newCol] = char.Parse(adjacentMines.ToString());
                    }
                    else
                    {
                        // Если снова 0 мин - открываем как пустую и продолжаем рекурсию
                        playerView[newRow, newCol] = ' ';
                        OpenAdjacentCells(newRow, newCol); // Рекурсивный вызов!
                    }
                }
            }
        }
    }
    static int CountAdjacentMines(int row, int col)
    {
        int count = 0;

        // Проверяем все 8 соседних клеток
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int newRow = row + i;
                int newCol = col + j;

                // Пропускаем текущую клетку и проверяем границы
                if ((i == 0 && j == 0) ||newRow < 0 || newRow >= 5 || newCol < 0 || newCol >= 5)
                {
                    continue;
                }

                if (mines[newRow, newCol])
                {
                    count++;
                }
            }
        }

        return count;
    }
    static void Pole()
    {

        for (int i = 0; i < mines.GetLength(0); i++)
        {
            for (int j = 0; j < mines.GetLength(1); j++)
            {
                if (mines[i, j] == true)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write(" " + mines[i, j] + "  ");

                    Console.ResetColor();
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write(" " + mines[i, j] + " ");

                    Console.ResetColor();
                }

            }
            Console.WriteLine();
        }

    }
    static void DisplayBoard()
    {
        Console.WriteLine("   A  B  C  D  E"); 

        for (int i = 0; i < 5; i++)
        {
            Console.Write($"{i + 1} ");

            for (int j = 0; j < 5; j++)
            {
                if (playerView[i, j] == '.')
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(" . ");
                }
                else if (playerView[i, j] == '*')
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write(" * ");
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write($" {playerView[i, j]} ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.ResetColor();
            }
            Console.WriteLine();
        }
        Pole();
    }

        static int InputNumber()
    {
        int number;
        while (!int.TryParse(Console.ReadLine(), out number) || number > 5)
        {
            if (number > 5)
            {
                ConsoleWriteLineColor(false, "Введите число меньше 4");
            }
            else
            {
                ConsoleWriteLineColor(false, "Введите число!!!!");
            }
        }
        return number;
    }
    static double InputGameNumber()
    {
        Console.Clear();
        ConsoleWriteLineColor(true, "Игра угадай число");
        Console.WriteLine("Введите значение А не равное 0:");
        double a;
        while (!double.TryParse(Console.ReadLine(), out a) || a == 0)
        {
            ConsoleWriteLineColor(false, "Введите числовое значение для А не равное 0:");
        }
        return a;
    }
    static double InpuOtvet()
    {
        double otvet;
        while (!double.TryParse(Console.ReadLine(), out otvet))
        {
            ConsoleWriteLineColor(false, "Вы ввели не число(");
        }
        return otvet;
    }
    static double Formula(double a)
    {
        const double eler = Math.E;
        double verx = (Math.Sin(a) + Math.Tan(2 * a));
        double niz = (Math.Log(Math.Pow(eler, 2), 3));
        if (niz < 0)
        {
            ConsoleWriteLineColor(false, "Ошибка: попытка извлечь корень из отрицательного числа!");
            BackToMenuTxt();
            Console.ReadKey();
        }
        niz = (Math.Sqrt(niz));
        double result = verx / niz;
        result = (Math.Round(result, 2));
        return result;
    }
    private static void Game(double result )
    {
        try
        {
            ConsoleWriteLineColor(true, "Попробуй угадать ответ за 3 попытки с округлением до 2-х знаков после запятой");
            for (int i = 0; i < 3; i++)
            {
                double otvet = InpuOtvet();


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
            throw;
        }
        catch (DivideByZeroException)
        {
            ConsoleWriteLineColor(false, "Деление на 0");
            BackToMenuTxt();
            Console.ReadKey();
            return;
        }
    }
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
        Array.Copy(originalArray, copyArray, originalArray.Length);
        return copyArray;
    }
    static void VivodVseh(int[] allArr)
    {
        if (allArr.Length > 10 == false)
        {
            foreach (int num in allArr)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine("\n");
        }
        else
        {
            ConsoleWriteLineColor(false, "Массив не возможно вывести так как больше 10");
        }
    }
    static void WriteArray()
    {
        int[] myArray = RandomArray();
       
        Console.WriteLine("Сгенерированный массив:");
        VivodVseh(myArray);
        Console.WriteLine("\n");

        int[] copyArr = CopyArr(myArray);
        Console.WriteLine("Скопированный массив:");
        VivodVseh(copyArr);
        Console.WriteLine("\n");

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Console.WriteLine("Сортировка пузырьком:");
        int[] sortbubble = SortBubble(myArray);
        stopwatch.Stop();
        var elapsedTime = stopwatch.Elapsed;
        Console.WriteLine(elapsedTime.ToString() + "\n");
        VivodVseh(sortbubble);

        Stopwatch stopwatch1 = new Stopwatch();
        stopwatch1.Start();
        Console.WriteLine("Сортировка выбором:");
        int[] min  = SortSelection(copyArr);
            
        stopwatch1.Stop();
        var elapsedTime1 = stopwatch1.Elapsed;
        Console.WriteLine(elapsedTime1.ToString()+"\n");
        VivodVseh(min);
        if (elapsedTime1 < elapsedTime)
        {
            Console.WriteLine("Сортировка выбором быстрее");
        }
        else
        {   
            Console.WriteLine("Сортировка пузырьком быстрее");
        }
        BackToMenuTxt();
        Console.ReadKey();
        Console.Clear();
    }
    static int[] SortBubble(int[] original)
    {
        int[] sortArray = original;
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
        int[] sortArray = copy;
        int n = copy.Length;
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
     public static bool ExiProgram()
    {
        Console.Clear();
        ConsoleWriteLineColor(true, "Вы дейсвительно хотите выйти: д|н");
        char yn;
        bool menu = true;
        bool vixod = true;
         while (char.TryParse(Console.ReadLine(), out yn) && vixod == true)
            {
                if (yn == 'д' || yn == 'н')
                {
                    if (yn == 'д')
                    {
                        menu = false;
                        vixod = false;
                        Console.WriteLine("нажмите что-то чтобы закрыть");
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