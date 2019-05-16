using System;
using System.Linq;
using System.Collections.Generic;

namespace Demo_Intern
{
    class Program
    {
        public static List<Product> Products = new List<Product>
        {
            new Product { SKU = "p01", Name = "iPad Pro 11-inch", Price = 23900 },
            new Product { SKU = "p02", Name = "Apple Watch Series 4", Price = 14400 },
            new Product { SKU = "p03", Name = "MacBook Pro with Touch Bar", Price = 47900 },
            new Product { SKU = "p04", Name = "Apple TV 4K", Price = 8500 },
            new Product { SKU = "p05", Name = "iPhone XS", Price = 39900 },
            new Product { SKU = "p06", Name = "iPhone XS Max", Price = 43900 },
            new Product { SKU = "p07", Name = "iPhone XR", Price = 29900 },
            new Product { SKU = "p08", Name = "MacBook Air 13-inch", Price = 42900 },
            new Product { SKU = "p09", Name = "Mac Mini 2018", Price = 27900 },
        };

        static void Main(string[] args)
        {
            var myCart = new List<Product>();
            var isPayment = false;

            while (!isPayment)
            {
                Console.Clear();

                // Show Products List
                Console.WriteLine("************ My Product *******************");
                Console.WriteLine("SKU|           Name             |     Price");
                Console.WriteLine("-------------------------------------------");
                foreach (var product in Products)
                {
                    Console.WriteLine($"{product.SKU}| {product.Name,-26} | {product.Price,9:N2}");
                }
                Console.WriteLine("*******************************************");
                Console.WriteLine();

                var totalAmount = myCart.Sum(it => it.Amount * it.Price);

                // Show Products in cart
                Console.WriteLine("Products in your cart are");
                if (myCart.Count > 0)
                {
                    for (int i = 0; i < myCart.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {myCart[i].Name,-26} ({myCart[i].Amount}) {myCart[i].Price,9:N2}");
                    }
                }
                else
                {
                    Console.WriteLine("none");
                }
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine($"Total amount: {totalAmount:N2} baht");
                Console.WriteLine();
                Console.Write("Please input a product key: ");

                var line = Console.ReadLine();
                var listText = line.Split(',');
                var p1 = listText.ElementAtOrDefault(0);
                var p2 = listText.ElementAtOrDefault(1);

                if (p1 == "payment")
                {
                    var money = int.Parse(p2 ?? "0");
                    if (money >= totalAmount)
                    {
                        Console.WriteLine($"Your change: {money - totalAmount:N2} bath");
                        Console.WriteLine("************* Thank you *******************");
                        isPayment = true;
                    }
                    else
                    {
                        Console.Write("Not enough money");
                        Console.ReadKey();
                    }
                }
                else
                {
                    var amount = int.Parse(p2 ?? "1");
                    if (myCart.Any(it => it.SKU == p1))
                    {
                        myCart.ForEach(it =>
                        {
                            if (it.SKU == p1) it.Amount += amount;
                        });
                    }
                    else
                    {
                        var pro = Products.Find(it => it.SKU == p1);
                        myCart.Add(new Product
                        {
                            SKU = pro.SKU,
                            Name = pro.Name,
                            Price = pro.Price,
                            Amount = amount
                        });
                    }
                }
            }
        }
    }

    public class Product
    {
        public string SKU { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
    }
}
