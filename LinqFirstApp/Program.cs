﻿using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using LinqFirstApp.Entities;
using System.Diagnostics;

namespace LinqFirstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AlgebraRelacionalWithSQL();
        }

        static void AlgebraRelacionalWithSQL()
        {
            Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category() { Id = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category() { Id = 3, Name = "Electronics", Tier = 1 };

            List<Product> products = new List<Product>() {
                new Product() { Id = 1, Name = "Computer", Price = 1100.0, Category = c2},
                new Product() { Id = 2, Name = "Hammer", Price = 90.0, Category = c1},
                new Product() { Id = 3, Name = "TV", Price = 1700.0, Category = c3},
                new Product() { Id = 4, Name = "Notebook", Price = 1300.0, Category = c2},
                new Product() { Id = 5, Name = "Saw", Price = 80.0, Category = c1},
                new Product() { Id = 6, Name = "Tablet", Price = 700.0, Category = c2},
                new Product() { Id = 7, Name = "Camera", Price = 700.0, Category = c3},
                new Product() { Id = 8, Name = "Printer", Price = 350.0, Category = c3},
                new Product() { Id = 9, Name = "MacBook", Price = 1800.0, Category = c2},
                new Product() { Id = 10, Name = "Sound Bar", Price = 700.0, Category = c3},
                new Product() { Id = 11, Name = "Level", Price = 70.0, Category = c1},
            };

            var r1 = from p in products where p.Category.Tier == 1 && p.Price < 900.00 select p;
            Print("\nAll the Tier 1 products that cost less than: R$ 900.00: ", r1);

            var r2 = from p in products where p.Category.Name == "Tools" select p.Name;
            Print("\nEvery Products in Tool's category: ", r2);

            var r3 = from p in products where p.Name.StartsWith("C") select p;
            Print("\nEvery Products starting their name with 'C': ", r3);

            var r4 = from p in products where p.Category.Tier == 1 orderby p.Name orderby p.Price select p;
            Print("\nOrding products of Tier 1 by theirs price, then their name: ", r4);

            var r5 = (from p in r4 select p).Skip(2).Take(4);
            Print("\nAll the tier 1 products ordered by their price, then by their name. It skip 2 and Take 4: ", r5);

            var r6 = (from p in products select p).FirstOrDefault();
            Console.WriteLine("\nThe first one or default: " + r6);
            var r7 = (from p in products where p.Price > 3000.00 select p).FirstOrDefault();
            if (r7 == null)
            {
                Console.WriteLine("\nThe first one or default (null): Null");
            }
            else
            {
                Console.WriteLine("\nThe first one or default (null): " + r7);
            }

            var r16 = from p in products group p by p.Category;
            foreach(var group in r16)
            {
                Console.WriteLine($"Category: {group.Key.Name}: ");
                foreach(Product p in group)
                {
                    Console.WriteLine($"{p}\n");
                }
            }
        }
        static void LinqWithLambda()
        {
            Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category() { Id = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category() { Id = 3, Name = "Electronics", Tier = 1 };

            List<Product> products = new List<Product>() {
                new Product() { Id = 1, Name = "Computer", Price = 1100.0, Category = c2},
                new Product() { Id = 2, Name = "Hammer", Price = 90.0, Category = c1},
                new Product() { Id = 3, Name = "TV", Price = 1700.0, Category = c3},
                new Product() { Id = 4, Name = "Notebook", Price = 1300.0, Category = c2},
                new Product() { Id = 5, Name = "Saw", Price = 80.0, Category = c1},
                new Product() { Id = 6, Name = "Tablet", Price = 700.0, Category = c2},
                new Product() { Id = 7, Name = "Camera", Price = 700.0, Category = c3},
                new Product() { Id = 8, Name = "Printer", Price = 350.0, Category = c3},
                new Product() { Id = 9, Name = "MacBook", Price = 1800.0, Category = c2},
                new Product() { Id = 10, Name = "Sound Bar", Price = 700.0, Category = c3},
                new Product() { Id = 11, Name = "Level", Price = 70.0, Category = c1},
            };

            var r1 = products.Where(p => p.Category.Tier == 1 && p.Price < 900.00);
            Print("\nAll the Tier 1 products that cost less than: R$ 900.00: ", r1);

            // Versão do tutor (Seleciona apenas os nomes):
            // var r2 = products.Where(p => p.Category.Name == "Tools").Select(p => p.Name);
            var r2 = products.Where(p => p.Category == c1);
            Print("\nEvery Products in Tool's category: ", r2);

            // Verão do tutor: (Seleciona os que começam com C e Objetos Anônimos):
            // var r3 = products.Where(p => p.Name[0] = 'C')
            // .Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name}); 
            var r3 = products.Where(p => p.Name.StartsWith("C"));
            Print("\nEvery Products starting their name with 'C': ", r3);

            // Aqui ele ordenará por preço e, se tiverem preços iguais, será por seu nome
            var r4 = products.Where(p => p.Category.Tier == 1).OrderBy(p => p.Price).ThenBy(p => p.Name);
            Print("\nOrding products by theirs price, then their name: ", r4);

            var r5 = r4.Skip(2).Take(4);
            Print("\nAll the tier 1 products ordered by their price, then by their name. It skip 2 and Take 4: ", r5);

            var r6 = products.FirstOrDefault();
            Console.WriteLine("\nThe first one or default: " + r6);
            var r7 = products.Where(p => p.Price > 3000).FirstOrDefault();
            Console.WriteLine("\nThe first one or default (null): " + r7);
            var r8 = products.Where(p => p.Id == 3).SingleOrDefault();
            Console.WriteLine("\nSingle or default (null): " + r8);
            var r9 = products.Where(p => p.Id == 30).SingleOrDefault();
            Console.WriteLine("\nSecond test with Single or default (null): " + r9);
            var r10 = products.Max(p => p.Price);
            Console.WriteLine("\nThe expensive one: R$ " + r10);
            var r11 = products.Min(p => p.Price);
            Console.WriteLine("\nThe cheapest one: R$ " + r11);
            var r12 = products.Where(p => p.Category.Id == 1).Sum(p => p.Price);
            Console.WriteLine("\nAll the category 1 sum prices: R$ " + r12);
            var r13 = products.Where(p => p.Category.Id == 1).Average(p => p.Price);
            Console.WriteLine("\nThe category 1 average prices: R$ " + r13);
            var r14 = products.Where(p => p.Category.Id == 5).Select(p => p.Price).DefaultIfEmpty(0.0).Average();
            Console.WriteLine("\nThe category 5 average prices: R$ " + r14);

            // Montando operações agregadas personalizadas (com map e reduce):
            var r15 = products.Where(p => p.Category.Id == 5).Select(p => p.Price).Aggregate(0.0, (x, y) => x + y);
            Console.WriteLine("\nAggregate sum in category 1: R$ " + r15);

            var r16 = products.GroupBy(p => p.Category);
            foreach (IGrouping<Category, Product> group in r16)
            {
                Console.WriteLine("\nCategory: " + group.Key.Name + ": ");
                foreach (Product product in group)
                {
                    Console.WriteLine($"{product}\n");
                }

            }

        }

        static void Print<T>(string message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
            foreach (T obj in collection)
            {
                Console.WriteLine(obj);
            }
        }
    }
}