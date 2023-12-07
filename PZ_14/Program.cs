
using System;
using System.IO;
namespace PZ_14
{
    internal class Program
    {
        static void Main(string[] args)
        {
                {
                    string file = "file.txt";

                    int k = 5;
                    int l = 8;

                    int[,] array = new int[k, l];

                    // Заполнение массива и расчет элементов по правилу A[i, j] = i * j
                    for (int i = 0; i < k; i++)
                    {
                        for (int j = 0; j < l; j++)
                        {
                            array[i, j] = i * j;
                        }
                    }

                    // Запись массива в файл с использованием потока записи
                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        for (int i = 0; i < k; i++)
                        {
                            for (int j = 0; j < l; j++)
                            {
                                writer.Write($"{array[i, j]} ");
                            }

                            writer.WriteLine();
                        }
                    }

                    Console.WriteLine("Запись в файл успешно выполнена.")ж
                }
            

        }
    }
}
    
