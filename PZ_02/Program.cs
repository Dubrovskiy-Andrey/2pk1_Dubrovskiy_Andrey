namespace PZ_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите число j");
            double  j = double.Parse(Console.ReadLine());

            double x;
            
            double y;

            double z;
            Console.WriteLine("Введите число i");
            int i = int.Parse(Console.ReadLine());

            if (j > 0)
                x = i * j + Math.Sin(j);
            else x = (i + j) / (Math.Sqrt(2 * j + i));
            if (x > 0)
                y = 15 * (Math.Sqrt(x - 0.5 * j)) + x;
            else y = Math.Cos(i + x);

            z = (x + y) / (Math.Pow(i, 2) + Math.Pow(j, 2) + 1);


            Console.WriteLine("i = " + i);
            Console.WriteLine("j = " + j);
            Console.WriteLine("x = " + Math.Round(x, 2));
            Console.WriteLine("y = " + Math.Round(y, 2));
            Console.WriteLine("z = " + Math.Round(z, 2));



        }
    }
}