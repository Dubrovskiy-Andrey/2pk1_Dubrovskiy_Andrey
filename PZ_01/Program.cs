namespace PZ_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            Console.WriteLine("Введите число a");
            double a = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите число b");
            double b = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите число c");
            double c = double.Parse(Console.ReadLine()); // объявили все константы
            double num1, num2, num3, num4, num5;
            num1 = Math.Pow(a, b + 1) + Math.Pow(Math.E, b - 1); // Делим число номер 1 на делитель и делимое и считаем по отдельности
            num2 = (1 + a * (Math.Abs(b - Math.Tan(c))));   
            num3 = (1 + Math.Abs(b - a));  // решаем число номер 2
            num4 = (Math.Pow(Math.Abs(b - a), 2)) / 2; // решаем число номер 3
            num5 = Math.Pow(Math.Abs(b - a), 3) / 3; // решаем число номер 4
            Console.WriteLine(num1 / num2 * num3 + num4 - num5);  // проводим все операции между числами    






        } 

    }
}