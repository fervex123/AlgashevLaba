using System;
using System.Security.Authentication;

namespace ConsoleApp1
{
    /// <summary>
    /// Класс для игры "Сапер"
    /// </summary>
    public class MinesweeperGame
    {
        /// <summary>
        /// Размер игрового поля
        /// </summary>
        public const int FIELD_SIZE = 5;

        /// <summary>
        /// Символ мины
        /// </summary>
        public const char MINE_SYMBOL = '*';

        /// <summary>
        /// Символ скрытой клетки
        /// </summary>
        public const char HIDDEN_CELL = '.';

        /// <summary>
        /// Символ пустой клетки
        /// </summary>
        public const char EMPTY_CELL = ' ';

        private bool[,] mines;
        private char[,] playerView;
        private int openedCells;
        private bool gameOver;

        /// <summary>
        /// Инициализирует новую игру
        /// </summary>
        public MinesweeperGame()
        {
            mines = new bool[FIELD_SIZE, FIELD_SIZE];
            playerView = new char[FIELD_SIZE, FIELD_SIZE];
            openedCells = 0;
            gameOver = false;
        }

        /// <summary>
        /// Запускает основной игровой цикл
        /// </summary>
        public void PlayGame()
        {
            GenerateMines();
            InitializePlayerView();

            while (!gameOver)
            {
                Console.Clear();
                ConsoleHelper.ConsoleWriteLineColor(true, "<<<<<ИГРА САПЁР>>>>>");
                DisplayBoard();

                (int row, int col) = GetPlayerInput();
                OpenCell(row, col);
            }

            Console.Clear();
            ConsoleHelper.ConsoleWriteLineColor(true, "<<<<<ИГРА САПЁР>>>>>");
            RevealAllMines();
            DisplayBoard();
            HandleGameOver();
        }

        /// <summary>
        /// Генерирует мины на поле
        /// </summary>
        private void GenerateMines()
        {
            Random rand = new Random();
            int minesCount = 0;

            // Инициализация поля
            for (int i = 0; i < FIELD_SIZE; i++)
            {
                for (int j = 0; j < FIELD_SIZE; j++)
                {
                    mines[i, j] = false;
                }
            }

            // Расстановка 5 мин
            while (minesCount < 5)
            {
                int i = rand.Next(0, FIELD_SIZE);
                int j = rand.Next(0, FIELD_SIZE);

                if (!mines[i, j])
                {
                    mines[i, j] = true;
                    minesCount++;
                }
            }
        }

        /// <summary>
        /// Инициализирует представление игрока
        /// </summary>
        private void InitializePlayerView()
        {
            for (int i = 0; i < FIELD_SIZE; i++)
            {
                for (int j = 0; j < FIELD_SIZE; j++)
                {
                    playerView[i, j] = HIDDEN_CELL;
                }
            }
        }

        /// <summary>
        /// Получает ввод координат от игрока
        /// </summary>
        /// <returns>Координаты (строка, столбец)</returns>
        private (int, int) GetPlayerInput()
        {
            while (true)
            {
                Console.Write("Введите координаты (например: A1): ");
                string input = Console.ReadLine()?.ToUpper() ?? "";

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Координаты не могут быть пустыми");
                    continue;
                }

                if (input.Length == 2 && char.IsLetter(input[0]) && char.IsDigit(input[1]))
                {
                    int col = input[0] - 'A';
                    int row = input[1] - '1';

                    if (row >= 0 && row < FIELD_SIZE && col >= 0 && col < FIELD_SIZE)
                    {
                        return (row, col);
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка: Координаты должны быть от A1 до {(char)('A' + FIELD_SIZE - 1)}{FIELD_SIZE}!");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: Неправильный формат! Используйте букву и цифру (например: A1)");
                }
            }
        }

        /// <summary>
        /// Открывает клетку по указанным координатам
        /// </summary>
        /// <param name="row">Строка</param>
        /// <param name="col">Столбец</param>
        private void OpenCell(int row, int col)
        {
            if (playerView[row, col] != HIDDEN_CELL)
            {
                Console.WriteLine("Эта клетка уже открыта!");
                Console.ReadKey();
                return;
            }

            if (mines[row, col])
            {
                playerView[row, col] = MINE_SYMBOL;
                Console.WriteLine("БОМБА! Вы проиграли!");
                gameOver = true;
                return;
            }

            int mineCount = CountAdjacentMines(row, col);
            playerView[row, col] = mineCount > 0 ? char.Parse(mineCount.ToString()) : EMPTY_CELL;
            openedCells++;

            if (mineCount == 0)
            {
                OpenAdjacentCells(row, col);
            }

            if (openedCells == FIELD_SIZE * FIELD_SIZE - 5) // Все клетки кроме мин
            {
                gameOver = true;
            }
        }

        /// <summary>
        /// Открывает соседние клетки для пустых областей
        /// </summary>
        /// <param name="row">Строка</param>
        /// <param name="col">Столбец</param>
        private void OpenAdjacentCells(int row, int col)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newCol = col + j;

                    if ((i == 0 && j == 0) || newRow < 0 || newRow >= FIELD_SIZE || newCol < 0 || newCol >= FIELD_SIZE)
                        continue;

                    if (playerView[newRow, newCol] == HIDDEN_CELL && !mines[newRow, newCol])
                    {
                        int adjacentMines = CountAdjacentMines(newRow, newCol);
                        playerView[newRow, newCol] = adjacentMines > 0 ? char.Parse(adjacentMines.ToString()) : EMPTY_CELL;
                        openedCells++;

                        if (adjacentMines == 0)
                        {
                            OpenAdjacentCells(newRow, newCol);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Подсчитывает количество мин вокруг клетки
        /// </summary>
        /// <param name="row">Строка</param>
        /// <param name="col">Столбец</param>
        /// <returns>Количество мин вокруг</returns>
        private int CountAdjacentMines(int row, int col)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newCol = col + j;

                    if ((i == 0 && j == 0) || newRow < 0 || newRow >= FIELD_SIZE || newCol < 0 || newCol >= FIELD_SIZE)
                        continue;

                    if (mines[newRow, newCol])
                        count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Отображает игровое поле
        /// </summary>
        private void DisplayBoard()
        {
            Console.Write("   ");
            for (char c = 'A'; c < 'A' + FIELD_SIZE; c++)
            {
                Console.Write($" {c} ");
            }
            Console.WriteLine();

            for (int i = 0; i < FIELD_SIZE; i++)
            {
                Console.Write($"{i + 1} ");
                for (int j = 0; j < FIELD_SIZE; j++)
                {
                    if (playerView[i, j] == HIDDEN_CELL)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write(" . ");
                    }
                    else if (playerView[i, j] == MINE_SYMBOL)
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
        }

        /// <summary>
        /// Показывает все мины на поле
        /// </summary>
        private void RevealAllMines()
        {
            for (int i = 0; i < FIELD_SIZE; i++)
            {
                for (int j = 0; j < FIELD_SIZE; j++)
                {
                    if (mines[i, j] && playerView[i, j] != MINE_SYMBOL)
                    {
                        playerView[i, j] = MINE_SYMBOL;
                    }
                }
            }
        }

        /// <summary>
        /// Обрабатывает завершение игры
        /// </summary>
        private void HandleGameOver()
        {
            if (openedCells == FIELD_SIZE * FIELD_SIZE - 5)
            {
                ConsoleHelper.ConsoleWriteLineColor(true, "ПОБЕДА! Вы открыли все безопасные клетки!");
            }
            else
            {
                ConsoleHelper.ConsoleWriteLineColor(false, "ПРОИГРЫШ! Вы наступили на мину!");
            }

            Console.WriteLine("Начать новую игру? y/n");
            char response;
            while (!char.TryParse(Console.ReadLine(), out response) || (response != 'y' && response != 'n'))
            {
                ConsoleHelper.ConsoleWriteLineColor(false, "Введите y или n:");
            }

            if (response == 'y')
            {
                // Перезапуск игры
                mines = new bool[FIELD_SIZE, FIELD_SIZE];
                playerView = new char[FIELD_SIZE, FIELD_SIZE];
                openedCells = 0;
                gameOver = false;
                PlayGame();
            }
            else
            {
                ConsoleHelper.BackToMenuTxt();
                Console.ReadKey();
            }
        }
    }
}