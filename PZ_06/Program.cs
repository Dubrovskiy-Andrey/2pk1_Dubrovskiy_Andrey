namespace PZ_06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[12];
            Random random = new Random();

            // Заполняем массив случайными числами в интервале [-12..12]
            for (int i = 0; i < 12; i++)
            {
                array[i] = random.Next(-12, 13);
            }

            Console.WriteLine("Исходный массив:");
            PrintArray(array);

            // Выполняем циклический сдвиг ВПРАВО на 4 элемента
            int[] shiftedArray = new int[12];
            for (int i = 0; i < 12; i++)
            {
                shiftedArray[(i + 4) % 12] = array[i];
            }

            Console.WriteLine("Сдвинутый массив:");
            PrintArray(shiftedArray);
        }

        static void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();
        }
    }
}