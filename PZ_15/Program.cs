using System;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine("Введите полный путь к каталогу:");
            string directoryPath = Console.ReadLine();

            // Получаем список файлов в указанном каталоге
            string[] files = Directory.GetFiles(directoryPath);

            Console.WriteLine("Список файлов:");
            for (int i = 0; i < files.Length; i++)
            {
                // Выводим только название файла (без расширения)
                Console.WriteLine(Path.GetFileNameWithoutExtension(files[i]));
            }

            Console.WriteLine("Введите название файла (без расширения):");
            string fileName = Console.ReadLine();

            string filePath = Path.Combine(directoryPath, fileName + ".txt");

            // Проверяем, существует ли файл
            if (File.Exists(filePath))
            {
                // Читаем содержимое файла и выводим его в консоль
                string fileContent = File.ReadAllText(filePath);
                Console.WriteLine("Содержимое файла:");
                Console.WriteLine(fileContent);
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка: " + ex.Message);
        }

        Console.ReadLine();
    }
}
