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

            var categories = new CategoryDTO[]
            {
                 new CategoryDTO{ID = 1, Description = "Init1", Discount = 10, Name = "Category1", CGST = 10, SGST = 10},
                 new CategoryDTO{ID = 2, Description = "Init2", Discount = 10, Name = "Category2", CGST = 20, SGST = 20}
            };

            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }

            var products = new ProductDTO[]
            {
                new ProductDTO{ID = 1, Name = "Product1", Description="Init1", RetailPrice = 120, WholeSalePrice = 130,
                           ImagePath="testpath1.jpg", CategoryID = 1},
                 new ProductDTO{ID = 2, Name = "Product2", Description="Init2", RetailPrice = 140, WholeSalePrice = 160,
                           ImagePath="testpath2.jpg", CategoryID = 2},
                  new ProductDTO{ID = 3, Name = "Product3", Description="Init3", RetailPrice = 140, WholeSalePrice = 160,
                           ImagePath="testpath3.jpg", CategoryID = 2}
            };

            foreach (var product in products)
            {
                context.Products.Add(product);
            }


            var stocks = new StockDTO[]
            {
                new StockDTO { ID=1, ProductID =1, AvailableQuantity=12, TotalQuantity=100},
                new StockDTO { ID=2, ProductID =2, AvailableQuantity=56, TotalQuantity=90},
                new StockDTO { ID=3, ProductID =3, AvailableQuantity=33, TotalQuantity=150}
            };
            foreach (var stock in stocks)
            {
                context.Stocks.Add(stock);
            }


            var customers = new CustomerDTO[]
            {
                new CustomerDTO { ID =1, MobileNumber = "7796351532", Email="abc@gmail.com", Name="Namrata Rawool", PendingAmount = 0, TotalAmount = 0 },
                new CustomerDTO {ID =2, MobileNumber = "8963457812", Email="def@gmail.com", Name="Aditya Bhende", PendingAmount = 0, TotalAmount = 0 }
            };
            foreach (var customer in customers)
            {
                context.Customers.Add(customer);
            }
            var transactions = new TransactionDTO[]
            {
                new TransactionDTO { ID = 1, CustomerID = 1, TotalPrice = 220, ProductIDs = "1,2", ProductQuantity="1,1", TransactionDateTime = DateTime.Parse(DateTime.Now.ToString() )},
                new TransactionDTO { ID = 2, CustomerID = 2, TotalPrice = 220, ProductIDs = "2,3", ProductQuantity="1,1",  TransactionDateTime = DateTime.Parse(DateTime.Now.ToString() )}
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
