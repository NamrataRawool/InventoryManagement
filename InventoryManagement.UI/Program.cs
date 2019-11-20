using InventoryManagement.DAL;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace InventoryManagement.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!File.Exists("InventoryDb.db"))
            {
                using (var db = new DatabaseContext())
                {
                    db.Database.EnsureCreated();
                }
            }
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
