using System.Runtime.Intrinsics.Arm;

namespace PZ_11
{
    internal class Program
    {
        static double GetAreaParallelogramm(double len, double heig)
        {
            return (len * heig);
        }
        static void Main(string[] args)
        {
            double L, h, L1,h1; // длина и высота параллелограмма
            double s, s1; // площадь поверхности параллелограмма
            Console.WriteLine(" Вычисление площади поверхности параллелограмма\n ");
            Console.WriteLine(" Введите исходные данные для первого параллелограмма:");
            Console.Write(" Длина (см) -> ");
            L = Convert.ToDouble(Console.ReadLine());
            Console.Write(" Высота (см) -> ");
            h = Convert.ToDouble(Console.ReadLine());
            s = GetAreaParallelogramm(L, h);
            Console.WriteLine(" Введите исходные данные для второго параллелограмма:");
            Console.Write(" Длина (см) -> ");
            L1 = Convert.ToDouble(Console.ReadLine());
            Console.Write(" Высота (см) -> ");
            h1 = Convert.ToDouble(Console.ReadLine());
            s1 = GetAreaParallelogramm(L1, h1);
            if (s>s1)
            {
                Console.WriteLine(" \nПлощадь первого параллелограмма больше второго и равна = {0,5:F2} кв.см.", s);
            }    
            else if (s<s1)
            {
                Console.WriteLine(" \nПлощадь второго параллелограмма больше первого и равна = {0,5:F2} кв.см.", s1);
            }
            else
            {
                Console.WriteLine(" \nПлощади первого и второго параллелограммов совпадают и равны = {0,5:F2} кв.см.", s);
            }
        }
    }
}