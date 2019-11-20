using InventoryManagement.DAL;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

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
                if (!File.Exists("InventoryDb.db"))
                {
                    try
                    {
                        using (var db = new DatabaseContext())
                        {
                            db.Database.EnsureCreated();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex, "An error occurred creating the DB.");
                    }
                }
                host.Run();
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
