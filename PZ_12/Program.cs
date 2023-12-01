namespace PZ_12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            static int CountSubstringOccurrences(string str, string subStr)
            {
                int count = 0;
                int lastIndex = 0;

                while ((lastIndex = str.IndexOf(subStr, lastIndex)) != -1)
                {
                    count++;
                    lastIndex += subStr.Length;
                }

                return count;
            }
            string str = Console.ReadLine();
            string subStr = Console.ReadLine();
            int Bxod = CountSubstringOccurrences(str, subStr);
            Console.WriteLine($"Количество вхождений '{subStr}' в строку '{str}': {Bxod}");
        }
    }
}