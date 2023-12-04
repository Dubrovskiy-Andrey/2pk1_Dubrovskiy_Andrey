namespace PZ_13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            static double ArithProgression(int n, double firstEl, double step)
            {
                if (n == 1)
                    return firstEl;

                double z = ArithProgression(n - 1, firstEl, step);
                return z + step;
            }

            
            {
                int n = Convert.ToInt32(Console.ReadLine()); // номер элемента, который нужно вычислить
                double firstEl = 9;
                double step = 0.5;

                double z = ArithProgression(n, firstEl, step);

                Console.WriteLine($"Значение {n}-го элемента арифметической прогрессии: {z}");
                Console.WriteLine(" ");
            }
            static double GeomProgression(int n, double firstEl, double znam)
            {
                if (n == 1)
                    return firstEl;

                double c = GeomProgression(n - 1, firstEl, znam);
                return c * znam;
            }

            
            {
                int v = Convert.ToInt32(Console.ReadLine()); ; // номер элемента, который нужно вычислить
                double firstEl = 5;
                double znam = -0.1;

                double b = GeomProgression(v, firstEl, znam);

                Console.WriteLine($"Значение {v}-го элемента геометрической прогрессии: {b}");
                Console.WriteLine(" ");
            }
            static void Print(int start, int end)
            {
                if (start == end)
                {
                    Console.WriteLine(end);
                    return;
                }

                if (start < end)
                {
                    Console.WriteLine(start);
                    Print(start + 1, end);
                }
                else
                {
                    Console.WriteLine(start);
                    Print(start - 1, end);
                }
            }

           
            {
                int start = 6;
                int end = 78;

                if (start < end)
                    Print(start, end);
                else
                    Print(start, end);
            }
            Console.WriteLine(" ");
            {
                Console.WriteLine("Введите число N: ");
                int N = Convert.ToInt32(Console.ReadLine()); 

                int sum = Summ(N);
                Console.WriteLine($"Сумма чисел от 1 до {N} равна {sum}");
            }

            static int Summ(int n)
            {
                if (n == 0)
                {
                    return 0;
                }
                else
                {
                    return n + Summ(n - 1);
                }
            }
        }

    }

    
}