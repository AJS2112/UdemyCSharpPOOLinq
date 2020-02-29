using System;
using System.Linq;
using ExLinq.Entities;
using System.Collections.Generic;
namespace ExLinq
{
    class Program
    {
        static void Print<T> (string message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
            foreach(T obj in collection)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category() { Id = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category() { Id = 3, Name = "Electronics", Tier = 1 };

            List<Product> products = new List<Product>()
            {
                new Product(){Id=1,Name="Computer",Price=1100.0,Category=c2},
                new Product(){Id=2,Name="Hammer",Price=90.0,Category=c1},
                new Product(){Id=3,Name="TV",Price=1700.0,Category=c3},
                new Product(){Id=4,Name="Notebook",Price=1300.0,Category=c2},
                new Product(){Id=5,Name="Saw",Price=80.0,Category=c1},
                new Product(){Id=6,Name="Tablet",Price=700.0,Category=c2},
                new Product(){Id=7,Name="Camera",Price=700.0,Category=c3},
                new Product(){Id=8,Name="Printer",Price=350.0,Category=c3},
                new Product(){Id=9,Name="MacBook",Price=1800.0,Category=c2},
                new Product(){Id=10,Name="SoundBar",Price=700.0,Category=c3},
                new Product(){Id=11,Name="Level",Price=70.0,Category=c1}
            };

            //LAMBDA Expressions
            //var r1 = products.Where(p => p.Category.Tier == 1 && p.Price < 900);
            var r1 = from p in products
                     where p.Category.Tier == 1 && p.Price < 900
                     select p;

            Print("TIER 1 AND PRICE < 900.0: ", r1);

            //var r2 = products.Where(p => p.Category.Name == "Tools").Select(p => p.Name);
            var r2 = from p in products
                     where p.Category.Name == "Tools"
                     select p.Name;
            Print("NAMES OF CATEGORY TOOLS: ", r2);

            //var r3 = products.Where(p => p.Name[0] == 'C').Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name });
            var r3 = from p in products
                     where p.Name[0] == 'C'
                     select new { p.Name, p.Price, CategoryName = p.Category.Name };
            Print("PRODUCT NAMES STARTED WITH 'C' AND ANONYMOUS OBJECT: ", r3);

            //var r4 = products.Where(p => p.Category.Tier == 1).OrderBy(p => p.Price).ThenBy(p => p.Name);
            var r4 = from p in products
                     where p.Category.Tier == 1
                     orderby p.Price, p.Name
                     select p;
            Print("TIER 1 ORDER BY PRICE THEN NAME: ", r4);

            //var r5 = r4.Skip(2).Take(4);
            var r5=  (from p in r4
                     select p).Skip(2).Take(4);
            Print("TIER 1 ORDER BY PRICE THEN NAME SKIP 2 AND TAKE 4: ", r5);

            //var r6 = products.First();
            var r6 = (from p in products select p).First();
            Console.WriteLine("FIRST: " + r6);

            //var r7 = products.Where(p => p.Price > 3000).FirstOrDefault();
            var r7 = (from p in products
                      where p.Price > 3000
                      select p).FirstOrDefault();
            Console.WriteLine("FIRST OR DEFAULT: " + r7);
            Console.WriteLine();

            var r8 = products.Where(p => p.Id == 3).SingleOrDefault();
            Console.WriteLine("SINGLE OR DEFAULT: " + r8);

            var r9 = products.Where(p => p.Id == 30).SingleOrDefault();
            Console.WriteLine("SINGLE OR DEFAULT Null: " + r9);
            Console.WriteLine();

            var r10 = products.Max(p => p.Price);
            Console.WriteLine("MAX PRICE: " + r10);

            var r11 = products.Max(p => p.Price);
            Console.WriteLine("MIN PRICE: " + r11);

            var r12 = products.Where(p => p.Category.Id==1).Sum(p => p.Price);
            Console.WriteLine("SUM PRICE CATEGORY 1: " + r12);

            var r13 = products.Where(p => p.Category.Id == 1).Average(p => p.Price);
            Console.WriteLine("AVG PRICE CATEGORY 1: " + r13);

            var r14 = products.Where(p => p.Category.Id == 5).Select(p => p.Price).DefaultIfEmpty(0.0).Average();
            Console.WriteLine("AVG PRICE CATEGORY 5: " + r14);

            //REDUCE
            var r15 = products.Where(p => p.Category.Id == 1).Select(p => p.Price).Aggregate(0.0, (x, y) => x + y);
            Console.WriteLine("CATEGORY 1 AGREGATES SUM: " + r15);
            Console.WriteLine();

            //var r16 = products.GroupBy(p => p.Category);
            var r16 = from p in products
                      group p by p.Category;                    
            foreach(IGrouping<Category,Product> group in r16)
            {
                Console.WriteLine("CATEGORY " + group.Key.Name + ":");
                foreach(Product product in group)
                {
                    Console.WriteLine(product);
                }
                Console.WriteLine();
            } 
        }
    }
}
