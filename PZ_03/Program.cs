namespace PZ_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
           
            int x = int.Parse(Console.ReadLine());
            int caseSwitch = x;

            switch (caseSwitch)
            {
                case 1:
                    Console.WriteLine(" Декабрь - 31 день, Январь - 31 день, Февраль - 28 дней");
                    break;
                case 2:
                    Console.WriteLine(" Март -31 день, Апрель - 30 дней, Май - 31 день ");
                    break;
                case 3:
                    Console.WriteLine(" Июнь - 30 дней, Июль 31 день, Август - 31 день ");
                    break;
                case 4:
                    Console.WriteLine(" Сентябрь - 30 дней, Октябрь - 31 день, Ноябрь - 30 дней ");
                    break;





            }

        }
    }
}