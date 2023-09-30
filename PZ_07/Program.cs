namespace PZ_07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] array = new int[7, 7];
            Random random = new Random();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    array[i, j] = random.Next(-10, 11);
                }
               
            }
            Console.WriteLine("Главная диагональ массива:");
            for (int i = 0; i < 7; i++)
            {
                Console.Write(array[i, i] + " ");
            }
            Console.WriteLine();

            // Подсчитываем количество положительных элементов на главной диагонали
            int l = 0;
            for (int i = 0; i < 7; i++)
            {
                if (array[i, i] > 0)
                {
                    l++;
                }
            }
            Console.WriteLine($"Количество положительных элементов на главной диагонали: {l}");
        }


    }
    
}