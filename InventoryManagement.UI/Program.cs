using InventoryManagement.Common.Models;
using InventoryManagement.UI.DAL;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InventoryManagement.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = CreateWebHostBuilder(args).Build();
            using (host)
            {
                Logger = host.Services.GetRequiredService<ILogger<Program>>();
                try
                {
                    using (var db = new InventoryDBContext())
                    {
                        db.Database.EnsureCreated();
                       // DBinitializer(db);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "An error occurred creating the DB.");
                }

                host.Run();
            }
        }
        private static void DBinitializer(InventoryDBContext context)
        {
            if (context.Transactions.Any())
                return;

            var taxes = new Tax[]
            {
                new Tax{TaxID = 1, CGST = 10, SGST = 10},
                new Tax{TaxID = 2, CGST = 20, SGST = 20},
            };
            foreach (var tax in taxes)
            {
                context.Taxes.Add(tax);
            }

            var categories = new Category[]
            {
                 new Category{CategoryID = 1, Description = "Init1", Discount = 10, Name = "Category1", TaxID = 1},
                 new Category{CategoryID = 2, Description = "Init2", Discount = 10, Name = "Category2", TaxID = 2}
            };

            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }

            var products = new Product[]
            {
                new Product{ProductID = 1, Name = "Product1", Description="Init1", RetailPrice = 120, WholeSalePrice = 130,
                            NoOfItems = 50, ImagePath="testpath1.jpg", CategoryID = 1},
                 new Product{Name = "Product2", Description="Init2", RetailPrice = 140, WholeSalePrice = 160,
                            NoOfItems = 150, ImagePath="testpath2.jpg", CategoryID = 2},
                  new Product{ Name = "Product3", Description="Init3", RetailPrice = 140, WholeSalePrice = 160,
                            NoOfItems = 150, ImagePath="testpath3.jpg", CategoryID = 2}
            };

            foreach (var product in products)
            {
                context.Products.Add(product);
            }
         
            var transactions = new Transaction[]
            {
                new Transaction { TransactionID = 1, TotalPrice = 220, ProductIDs = "1,2"},
                new Transaction { TransactionID = 2, TotalPrice = 220, ProductIDs = "2,3"}
            };

            foreach (var transaction in transactions)
            {
                context.Transactions.Add(transaction);
            }

            //var prodtrans = new ProductTransaction[]
            //{
            //    new ProductTransaction {TransactionID = 1, ProductID =2},
            //    new ProductTransaction {TransactionID = 1, ProductID =1},
            //    new ProductTransaction {TransactionID = 2, ProductID =1},
            //    new ProductTransaction {TransactionID = 2, ProductID =2}
            //};
            //foreach (var item in prodtrans)
            //{
            //    context.ProductTransactions.Add(item);
            //}

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        private static ILogger Logger
        {
            get;
            set;
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
