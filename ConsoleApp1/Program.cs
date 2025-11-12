
using ConsoleApp1;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{

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
            ConsoleHelper.ConsoleWriteLineColor(true, "Выберите что-то из списка");
            Console.Write("1 - Отгадай ответ\n2 - Об авторе\n3 - Сортировка массива \n4 - Сапер\n5 - Выход\n");
            int number= StaticFunc.InputNumber();
            switch (number)
            {
                case 1:
                    double a = StaticFunc.InputGameNumber();
                    double result = GameOtvet.Formula(a);
                    GameOtvet.Game(result);
                    ConsoleHelper.BackToMenuTxt();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 2:
                    Other.Avtor();
                    break;
                case 3:
                    Console.Clear();
                    ArrayWorker arr= new ArrayWorker();
                    arr.WriteArray();
                    break;
                case 4:
                    Console.Clear();
                    MinesweeperGame game = new MinesweeperGame();
                    game.PlayGame();
                    break;
                case 5:
                    menu = Other.ExiProgram();
                    break;
            }
        }
    }
}