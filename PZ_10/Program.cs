namespace PZ_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text = "Я люблю осень. Осень является прекрасным временем года. Почему? Потому что осенью все листья разноцветные и красивые. А также частый дождь. Запах после дождя очень приятный! Люблю осень.";

            // Разделение текста на предложения
            string[] sentences = text.Split(new[] { '.', '!', '?' });

            // Удаление пробелов в начале и конце предложений
            sentences = sentences.Select(s => s.Trim()).ToArray();

            // Создание списка пар "предложение-количество слов"
            List<Tuple<string, int>> sentenceCountList = new List<Tuple<string, int>>();
            foreach (var sentence in sentences)
            {
                int wordCount = sentence.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
                sentenceCountList.Add(new Tuple<string, int>(sentence, wordCount));
            }

            // Сортировка списка по количеству слов
            sentenceCountList = sentenceCountList.OrderBy(t => t.Item2).ToList();

            // Вывод отсортированных предложений
            foreach (var tuple in sentenceCountList)
            {
                Console.WriteLine(tuple.Item1);
            }
        }
    }
}