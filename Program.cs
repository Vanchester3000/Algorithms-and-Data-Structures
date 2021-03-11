using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

/*
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i5-7400 CPU 3.00GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
.NET Core SDK=5.0.103
  [Host]     : .NET Core 3.1.12 (CoreCLR 4.700.21.6504, CoreFX 4.700.21.6905), X64 RyuJIT
  DefaultJob : .NET Core 3.1.12 (CoreCLR 4.700.21.6504, CoreFX 4.700.21.6905), X64 RyuJIT


|                         Method |      Mean |     Error |    StdDev |
|------------------------------- |----------:|----------:|----------:|
|          'Дистанция ссылочная' | 45.045 ns | 0.5167 ns | 0.4834 ns |
|         'Дистанция значимая f' |  4.093 ns | 0.0188 ns | 0.0166 ns |
|         'Дистанция значимая d' |  4.091 ns | 0.0289 ns | 0.0270 ns |
| 'Дистанция значимая без корня' |  4.134 ns | 0.0250 ns | 0.0234 ns |
*/


namespace Algorithms_and_Data_Structures
{
    class Program
    {
        public PointClass pointClass = new PointClass() { X = 42, Y = 42 };
        public PointClass pointClass2 = new PointClass() { X = 58, Y = 47 };
        static void Main(string[] args)
        {
            
            Console.ForegroundColor = ConsoleColor.Green;
            
            BenchmarkSwitcher.FromAssembly(typeof(BenchmarkClass).Assembly).Run(args);

        }
    }


    public class PointClass //ссылочный тип float
    {
        public float X;
        public float Y;
    }
    public struct PointStructFloat //значимый тип float
    {
        public float X;
        public float Y;
    }
    public struct PointStructDouble //значимый тип Double
    {
        public double X;
        public double Y;
    }


    public class BenchmarkClass
    {
        public float[] floatArray = { 44, 21, 12, 46, 88 };
        public float[] floatArray2 = { 35, 64, 98, 15, 78 };


        public static float PointDistanceClass(PointClass pointOne, PointClass pointTwo) //вычисление дистанции для ссылочного типа float
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return MathF.Sqrt((x * x) + (y * y));
        }
        [Benchmark(Description = "Дистанция ссылочная")]
        public void testDistanceClass()
        {
            for (int i = 0; i < 5; i++)
            {
                var pointOne = new PointClass() { X = floatArray[i], Y = floatArray[i] };
                var pointTwo = new PointClass() { X = floatArray2[i], Y = floatArray2[i] };

                var res = PointDistanceClass(pointOne, pointTwo);
                //Console.WriteLine($"Результат метода {i + 1}: {res}");
            }
        }


        public static float PointDistanceStructFloat(PointStructFloat pointOne, PointStructFloat pointTwo) //вычисление дистанции для значимого типа float
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return MathF.Sqrt((x * x) + (y * y));
        }
        [Benchmark(Description = "Дистанция значимая f")]
        public void testDistanceStructFloat()
        {
            for (int i = 0; i < 5; i++)
            {
                var pointOne = new PointStructFloat() { X = floatArray[i], Y = floatArray[i] };
                var pointTwo = new PointStructFloat() { X = floatArray2[i], Y = floatArray2[i] };

                var res = PointDistanceStructFloat(pointOne, pointTwo);
            }
        }


        public static double PointDistanceStructDouble(PointStructDouble pointOne, PointStructDouble pointTwo) //вычисление дистанции для значимого типа double
        {
            double x = pointOne.X - pointTwo.X;
            double y = pointOne.Y - pointTwo.Y;
            return Math.Sqrt((x * x) + (y * y));
        }
        [Benchmark(Description = "Дистанция значимая d")]
        public void testDistanceStructDouble()
        {
            for (int i = 0; i < 5; i++)
            {
                var pointOne = new PointStructDouble() { X = floatArray[i], Y = floatArray[i] };
                var pointTwo = new PointStructDouble() { X = floatArray2[i], Y = floatArray2[i] };

                var res = PointDistanceStructDouble(pointOne, pointTwo);
            }
        }


        public static float PointDistanceShort(PointStructFloat pointOne, PointStructFloat pointTwo) //вычисление дистанции для значимого типа float без квадратного корня
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return (x * x) + (y * y);
        }
        [Benchmark(Description = "Дистанция значимая без корня")]
        public void testDistanceShort()
        {
            for (int i = 0; i < 5; i++)
            {
                var pointOne = new PointStructFloat() { X = floatArray[i], Y = floatArray[i] };
                var pointTwo = new PointStructFloat() { X = floatArray2[i], Y = floatArray2[i] };

                var res = PointDistanceShort(pointOne, pointTwo);
            }
        }
    }
}