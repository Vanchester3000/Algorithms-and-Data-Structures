using System;

namespace Algorithms_and_Data_Structures
{
    class Program
    {
        static void Cleaning(string text)
        {
            if (text == "очистить")
            {
                Console.Clear();
            }
        }

        #region 1 задание
        //static int count = 0;
        static void PrimeNumber(int number)
        {
            int i = 2;
            int d = 0;

            while (i < number)
            {
                if (number % i == 0)
                {
                    d++;
                }
                i++;
            }
            if (d == 0)
            {
                Console.Write($"Число {number} простое.   ");
            }
            else
            {
                Console.Write($"Число {number} не простое.   ");
            }


            #region Забыл, что надо было по блоксхеме, но удалять жалко поэтому просто закомичу и добавлю в регион

            //if (number <= 1)
            //{
            //    Console.WriteLine("Простых чисел до этого числа не имеется");
            //    return;
            //}

            //for (int i = 0; i <= number; i++)
            //{
            //    count = 0;
            //    for (int j = 1; j <= i; j++)
            //    {
            //        if (i % j == 0)
            //        {
            //            ++count;
            //        }
            //    }
            //    if (count == 2)
            //    {
            //        Console.Write(i + " ");
            //    }

            //}
            //Console.WriteLine();

            #endregion
        }
        #endregion

        #region 2 задание
        //не особо понял насчёт второго но ответ: О(N*N*N)
        public static int StrangeSum(int[] inputArray)
        {
            int sum = 0;
            for (int i = 0; i < inputArray.Length; i++)
            {
                for (int j = 0; j < inputArray.Length; j++)
                {
                    for (int k = 0; k < inputArray.Length; k++)
                    {
                        int y = 0;

                        if (j != 0)
                        {
                            y = k / j;
                        }

                        sum += inputArray[i] + i + k + j + y;
                        Console.Write(sum + " ");
                    }
                    Console.Write("\n" + sum + " ");
                }
                Console.Write("\n" + sum + " ");
            }
            Console.Write("\n" + sum + " ");

            return sum;
        }
        #endregion

        #region 3 задание
        //Рекурсия
        static int FibbonachiRc(int n)
        {
            return (n == 1 || n == 0) ? 1 : FibbonachiRc(n - 1) + FibbonachiRc(n - 2);
        }
        //Метод
        static int FibbonachiMe(int n)
        {
            int fib1;
            int fib2 = 1;
            int fibbonachi = 0;

            for (int i = 0; i <= n; i++)
            {
                fib1 = fib2;
                fib2 = fibbonachi;
                fibbonachi = fib1 + fib2;
            }
            return fibbonachi;
        }

        #endregion

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            #region 1 задание проверка
            PrimeNumber(2);
            PrimeNumber(7);
            PrimeNumber(14);
            PrimeNumber(52);
            PrimeNumber(101);

            //Проверочные значения
            Console.Write("\nЧисло 2 простое.   Число 7 простое.   Число 14 не простое.   Число 52 не простое.   Число 101 простое.");

            #region это проверка к тому что сделал без схемы

            ////Пропишите значение до которого надо показать простые числа
            //PrimeNumber(200);

            ////значения в массиве взяты из гугла по поиску "простые числа"
            //int[] arr = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199 };

            //for (int i = 0; i < arr.Length; i++)
            //{
            //    Console.Write(arr[i] + " ");
            //}
            //Console.WriteLine();

            #endregion
            #endregion

            Console.WriteLine("\nДля очичения консоли напишите: очистить");
            Cleaning(Console.ReadLine());

            #region 3 задание проверка
            //Рекурсия
            Console.WriteLine("Тут придётся подождать т.к. рекурсия ооооооооооочень медленная");
            Console.WriteLine(FibbonachiRc(5) + " " + FibbonachiRc(15) + " " + FibbonachiRc(28) + " " + FibbonachiRc(37) + " " + FibbonachiRc(45));
            //Метод
            Console.WriteLine(FibbonachiMe(5) + " " + FibbonachiMe(15) + " " + FibbonachiMe(28) + " " + FibbonachiMe(37) + " " + FibbonachiMe(45));
            //Что должно вывести
            Console.WriteLine("8 987 514229 39088169 1836311903");

            #endregion
        }
    }
}