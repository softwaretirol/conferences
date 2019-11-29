using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCore1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await using var ctx = new ErpContext();
            await ctx.Database.MigrateAsync();

            await ctx.Products.IgnoreQueryFilters().ToListAsync();

            //var product2 = await ctx.Products.FirstOrDefaultAsync(x => x.Id == 3);
            //var product3 = await ctx.Products.SingleOrDefaultAsync(x => x.Id == 3);
            //var product1 = await ctx.Products.FindAsync(3);

            var product1 = new Product()
            {
                Id = 3
            };
            product1.Preis = 123;

            var entry = ctx.Attach(product1);
            entry.State = EntityState.Modified;
            await ctx.SaveChangesAsync();
            Console.ReadLine();


            //for (int i = 0; i < 100; i++)
            //{
            //    await ctx.Products.AddAsync(new Product()
            //    {
            //        Name = "Kaiserschmarrn",
            //        Preis = 12
            //    });
            //}

            //ctx.Database.ExecuteSqlRawAsync()
            //foreach (var product in ctx.Products)
            //{
            //    product.Preis *= (decimal) 1.05;
            //}

            //await ctx.SaveChangesAsync();


            var query1 = await ctx
                .Order
                .Select(x => new
                {
                    x.OrderDate,
                    ProductNames = x.OrderProduct.Select(y => y.Product.Name)
                }).ToListAsync();
            var query2 = await ctx
                .OrderProduct
                .Select(x => new
                {
                    x.Order.OrderDate,
                    ProductNames = x.Product.Name
                }).ToListAsync();


            //foreach (var item in query2)
            //{
            //    item.ProductNames = string.Empty;
            //}

            var allOrders = await ctx
                .Order
                //.Include(x => x.OrderProduct)
                //.ThenInclude(x => x.Product)
                .ToListAsync();
            foreach (var order in allOrders)
            {
                Console.WriteLine(order.OrderDate);
                foreach (var orderProduct in order.OrderProduct)
                {
                    Console.WriteLine(orderProduct.Product.Name);
                }
            }

            Console.ReadLine();

            //await ctx.Order.AddAsync(new Order()
            //{
            //    OrderDate = DateTime.Now,
            //    OrderProduct = new List<OrderProduct>()
            //    {
            //        new OrderProduct()
            //        {
            //            Product = new Product()
            //            {
            //                Name = "Kein Wiener Schnitzel"
            //            }
            //        }
            //    }
            //});


            //await foreach (var product in ctx.Products)
            //{
            //    Console.WriteLine(product.Name);
            //    //product.Name = product.Name + " " + product.Name;
            //}

            await ctx.SaveChangesAsync();
            Console.ReadLine();

            //await ctx.Database.MigrateAsync();
            //List<Product> query = await ctx
            //        .Products
            //        .Where(x => x.Id % 2 == 0)
            //        .ToListAsync();

            //query[0].Name = query[0].Name.ToUpper();

            //ctx.Products.Remove(query[0]);
            //await ctx.Database.EnsureCreatedAsync();

            //var product = new Product()
            //{
            //    Name = "Wiener Schnitzel",
            //    Preis = 42,
            //    Gewicht = 500
            //};

            //await ctx.Products.AddAsync(product);
            //var entries = ctx.ChangeTracker.Entries();

            //await ctx.SaveChangesAsync();
            //Console.WriteLine(product.Id);
        }

        private static bool IsIntrestingProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
