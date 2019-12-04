using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InventoryDBManagement.DAL;
using InventoryManagement.Common.Models;
using InventoryManagement.Common.Models.DTO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InventoryDBManagement
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

            //var taxes = new Tax[]
            //{
            //    new Tax{TaxID = 1, CGST = 10, SGST = 10},
            //    new Tax{TaxID = 2, CGST = 20, SGST = 20},
            //};
            //foreach (var tax in taxes)
            //{
            //    context.Taxes.Add(tax);
            //}

            var categories = new Category[]
            {
                 new Category{CategoryID = 1, Description = "Init1", Discount = 10, Name = "Category1", CGST = 10, SGST = 10},
                 new Category{CategoryID = 2, Description = "Init2", Discount = 10, Name = "Category2", CGST = 20, SGST = 20}
            };

            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }

            var products = new ProductDTO[]
            {
                new ProductDTO{ProductID = 1, Name = "Product1", Description="Init1", RetailPrice = 120, WholeSalePrice = 130,
                           ImagePath="testpath1.jpg", CategoryID = 1},
                 new ProductDTO{Name = "Product2", Description="Init2", RetailPrice = 140, WholeSalePrice = 160,
                           ImagePath="testpath2.jpg", CategoryID = 2},
                  new ProductDTO{ Name = "Product3", Description="Init3", RetailPrice = 140, WholeSalePrice = 160,
                           ImagePath="testpath3.jpg", CategoryID = 2}
            };

            foreach (var product in products)
            {
                context.Products.Add(product);
            }

            var transactions = new Transaction[]
            {
                new Transaction { TransactionID = 1, TotalPrice = 220, ProductIDs = "1,2", TransactionDateTime = DateTime.Parse(DateTime.Now.ToString() )},
                new Transaction { TransactionID = 2, TotalPrice = 220, ProductIDs = "2,3", TransactionDateTime = DateTime.Parse(DateTime.Now.ToString() )}
            };

            foreach (var transaction in transactions)
            {
                context.Transactions.Add(transaction);
            }

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
