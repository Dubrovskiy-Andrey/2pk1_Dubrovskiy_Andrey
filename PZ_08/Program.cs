namespace PZ_08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Задаем размеры ступенчатого массива
            int[][] array = new int[8][];
            Random random = new Random();

            // Заполняем массив случайными числами и выводим его
            Console.WriteLine("Исходный массив:");
            for (int i = 0; i < array.Length; i++)
            {
                int length = random.Next(6, 11); // Генерируем рандомную длину второго измерения
               array[i] = new int[length];
                for (int j = 0; j < array[i].Length; j++)
                {
                    array[i][j] = random.Next(1, 101); // Генерируем случайное число от 1 до 100
                    Console.Write(array[i][j] + " ");
                }
                Console.WriteLine();              
            }
            Console.WriteLine();
            // Создаем и заполняем новый одномерный массив последними элементами каждой строки
            int[] Last = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                Last[i] = array[i][array[i].Length - 1];
            }

            // Выводим новый массив
            Console.WriteLine("Массив последних элементов каждой строки:");
            for (int i = 0; i < Last.Length; i++)
            {
                Console.WriteLine(Last[i]);
            }
            Console.WriteLine();
            // Находим максимальный элемент в каждой строке ступенчатого массива и записываем их в другой массив
            int[] max = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                int maxElement = array[i][0];
                for (int j = 1; j < array[i].Length; j++)
                {
                    if (array[i][j] > maxElement)
                    {
                        maxElement = array[i][j];
                    }
                }
                max[i] = maxElement;
            }
            // Выводим массив максимальных элементов
            Console.WriteLine("Массив максимальных элементов каждой строки:");
            for (int i = 0; i < max.Length; i++)
            {
                Console.WriteLine(max[i]);
            }
            Console.WriteLine();
            // Меняем местами первый элемент и максимальный в каждой строке
            for (int i = 0; i < array.Length; i++)
            {
                int temp = array[i][0];
                array[i][0] = max[i];
                max[i] = temp;
            }

            // Выводим обновленный массив
            Console.WriteLine("Обновленный массив после замены первого элемента и максимального в каждой строке:");
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    Console.Write(array[i][j] + " ");
                }
                Console.WriteLine();
                
            }
            Console.WriteLine();
            // Реверсируем каждую строку ступенчатого массива
            for (int i = 0; i < array.Length; i++)
            {
                Array.Reverse(array[i]);
            }

            // Выводим массив после реверса строк
            Console.WriteLine("Массив после реверса строк:");
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    Console.Write(array[i][j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            // Подсчитываем среднее значение в каждой строке
            Console.WriteLine("Средние значения в каждой строке:");
            for (int i = 0; i < array.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < array[i].Length; j++)
                {
                    sum += array[i][j];
                }
                double q = sum / array[i].Length;
                Console.WriteLine(q);
            }
            Console.WriteLine();
            // Подсчитываем общее количество символов в строках каждой строки массива
            Console.WriteLine("Общее количество символов в каждой строке массива:");
            for (int i = 0; i < array.Length; i++)
            {
                int total = 0;
                for (int j = 0; j < array[i].Length; j++)
                {
                    total += array[i][j].ToString().Length;
                }
                Console.WriteLine(total);
            }
            Console.WriteLine();
            // Находим наиболее встречающийся символ в каждой строке ступенчатого массива
            Console.WriteLine("Наиболее встречающиеся символы в каждой строке:");
            for (int i = 0; i < array.Length; i++)
            {
                string row = string.Join("", array[i]);
                char x = FindMostFrequentCharacter(row);
                Console.WriteLine(x);
            }
        }

        // Метод для нахождения наиболее встречающегося символа в строке
        static char FindMostFrequentCharacter(string text)
        {
            int[] charCount = new int[256];

            foreach (char c in text)
            {
                charCount[c]++;
            }

            int maxCount = 0;
            char mostFrequentChar = '\0';

            for (int i = 0; i < charCount.Length; i++)
            {
                if (charCount[i] > maxCount)
                {
                    maxCount = charCount[i];
                    mostFrequentChar = (char)i;
                }
            }

            return mostFrequentChar;
        }

    }   
}