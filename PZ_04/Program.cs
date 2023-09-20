namespace PZ_04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int i;
            Console.WriteLine("Задание 1");
            for(i = 0; i <= 60; i = i+5)
            {
                Console.WriteLine(i);
            
                    }


            //
            //
            Console.WriteLine();
            Console.WriteLine("Задание 2");
            
            
           for (char k = 'e'; k < 'j'; k++)
            Console.Write(k + " ");
            Console.WriteLine();
            //
            //
            Console.WriteLine();
            Console.WriteLine("Задание 3");
            for (int l = 0; l < 6; l++) 
            {

                Console.WriteLine();
                for (int j = 0; j < 3; j++) 
                {
                   
                    Console.Write("#");
                    
                }
            }
            //
            //
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Задание 4");
            int count = 0;
            for (int h = 0; h <= 1000; h++)
                if (h % 5 == 0)
                {
                    Console.Write(h + " ");
                    count++;
                    
                }
            Console.WriteLine() ;
            Console.WriteLine(count + "- Количество чисел");
            //
            //
            Console.WriteLine();
            Console.WriteLine("Задание 5");

            for (int o = 2, p = 40; o - p <= 18; o++, p--)
            {
                Console.WriteLine(o + " - o " + p + " - p");
            }
            

        }
        
    }
}