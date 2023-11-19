using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace PZ_09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите слово для проверки:");
        string word = Console.ReadLine();

        bool isIdentifier = IsIdentifier(word);

        if (isIdentifier)
        {
            Console.WriteLine("Введенное слово является идентификатором");
        }
        else
        {
            Console.WriteLine("Введенное слово не является идентификатором");
        }

        Console.ReadKey();
    }

    static bool IsIdentifier(string word)
    {
        // Проверяем, что слово не пустое
        if (string.IsNullOrEmpty(word))
        {
            return false;
        }

        // Проверяем, что первый символ - буква английского алфавита или символ подчеркивания
        char first = word[0];
        if (!char.IsLetter(first) && first != '_')
        {
            return false;
        }

        // Проверяем, что остальные символы состоят только из букв английского алфавита или цифр или символа подчеркивания
        string pattern = @"^[a-zA-Z0-9_]+$";
        return Regex.IsMatch(word, pattern);
        }
    }
}
