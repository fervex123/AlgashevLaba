using System;
using System.Diagnostics;

namespace ConsoleApp1
{
    /// <summary>
    /// Класс для работы с массивами
    /// </summary>
    public class ArrayWorker
    {
         private static int ArrayLengthMethod()
        {
            int arrayLng;
            ConsoleHelper.ConsoleWriteLineColor(true, "Введите длину массива не длинее 10");
            while (!int.TryParse(Console.ReadLine(), out arrayLng) || arrayLng < 1)
            {
                ConsoleHelper.ConsoleWriteLineColor(false, "Введите другую длину массива");
            }
            return arrayLng;
        }
        private static int[] RandomArray()
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
        private static int[] CopyArr(int[] originalArray)
        {
            int[] copyArray = new int[originalArray.Length];
            Array.Copy(originalArray, copyArray, originalArray.Length);
            return copyArray;
        }
        private static void VivodVseh(int[] allArr)
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
                ConsoleHelper.ConsoleWriteLineColor(false, "Массив не возможно вывести так как больше 10");
            }
        }
        public void WriteArray()
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
            int[] min = SortSelection(copyArr);

            stopwatch1.Stop();
            var elapsedTime1 = stopwatch1.Elapsed;
            Console.WriteLine(elapsedTime1.ToString() + "\n");
            VivodVseh(min);
            if (elapsedTime1 < elapsedTime)
            {
                Console.WriteLine("Сортировка выбором быстрее");
            }
            else
            {
                Console.WriteLine("Сортировка пузырьком быстрее");
            }
            ConsoleHelper.BackToMenuTxt();
            Console.ReadKey();
            Console.Clear();
        }
        private static int[] SortBubble(int[] original)
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
        private static int[] SortSelection(int[] copy)
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
    }
}