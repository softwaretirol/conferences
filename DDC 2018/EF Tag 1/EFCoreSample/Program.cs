using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new EF1Ef6ContextContext())
            {
                //ctx.Database.Log
                ctx.Customers.FirstOrDefault(x => x.Id == -1);

                var watch = Stopwatch.StartNew();
                //ctx.Customers.Select(x => new
                //{
                //    x.CompanyName
                //}).ToList();

                //List<Customers> customersToAdd = new List<Customers>();
                // 2.4 --> 10000
                for (int i = 0; i < 2; i++)
                {
                    ctx.Customers.Add(new Customers()
                    {
                        CompanyName = i.ToString()
                    });
                }

                //ctx.Customers.AddRange(customersToAdd);
                ctx.SaveChanges();

                Console.WriteLine(watch.Elapsed);
                Console.ReadLine();
            }

            Console.WriteLine("Hello World!");
        }
    }
}
