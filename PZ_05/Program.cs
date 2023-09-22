namespace PZ_05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите число N: ");
            int N = int.Parse(Console.ReadLine());

            Console.WriteLine("Целые степени двойки, не превосходящие N:");

            int a = 1;

            while (a <= N)
            {
                Console.WriteLine(a);
                a = a << 1; // умножение на 2 через сдвиг
            }
        }
        
    }
}