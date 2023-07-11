using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace LinqFirstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Specify the data source
            int[] numbers = new int[] { 1, 2, 3, 4, 5 };

            // Define the query expression
            // Variável que recebe do numbers
            // os valores x ONDE o resto de
            // divisão por 2 é 0
            IEnumerable<int> result = numbers
                .Where(x => x % 2 == 0)
                .Select(x => x * 10);

            // Execute the query
            Console.Write("[");
            foreach (int x in result)
            {
                Console.Write($" {x} ");
            }
            Console.Write("]");
        }
    }
}